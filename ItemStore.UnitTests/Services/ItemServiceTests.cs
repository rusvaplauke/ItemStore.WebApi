using Moq;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Services;
using FluentAssertions;
using AutoMapper;
using ItemStore.WebApi.Profiles;
using ItemStore.WebApi.Exceptions;
using AutoFixture;
using AutoFixture.Xunit2;
using ItemStore.WebApi.Models.DTOs.ItemDtos;

namespace ItemStore.UnitTests.Services;

public class ItemServiceTests
{
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public ItemServiceTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<ItemProfile>()));

        _itemService = new ItemService(_itemRepositoryMock.Object, _mapper);
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenValidId_ReturnsDto(int id, string name, decimal price)
    {
        //Arrange
        ItemEntity item = new ItemEntity {Id = id, Name = name, Price = price};

        _itemRepositoryMock.Setup(m => m.Get(id)).ReturnsAsync(item);

        //Act
        GetItemDto result = await _itemService.Get(item.Id); 

        //Assert
        result.Id.Should().Be(item.Id);
        result.Name.Should().Be(item.Name);
        result.Price.Should().Be(item.Price);
    }

    [Fact]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        int id = _fixture.Create<int>();

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async() => await _itemService.Get(id));
    }

    [Fact]
    public async Task Get_GivenNoId_ReturnsAllDtos()
    {
        //Arrange
        List<ItemEntity> list = new List<ItemEntity>();
        _fixture.AddManyTo(list,5);

        _itemRepositoryMock.Setup(m => m.Get()).ReturnsAsync(list);

        //Act
        List<GetItemDto> result = await _itemService.Get();

        //Assert
        result.Count().Should().Be(list.Count());
    }

    [Fact]
    public async Task Create_GivenPosItemDto_ReturnsGetItemDto()
    {
        //Arrange
        PostItemDto request = _fixture.Create<PostItemDto>();
        ItemEntity itemForRepo = _fixture.Build<ItemEntity>().With(x => x.Name, request.Name).With(x => x.Price, request.Price).Create();

        _itemRepositoryMock.Setup(m => m.Create(It.IsAny<ItemEntity>())).ReturnsAsync(itemForRepo.Id); 

        _itemRepositoryMock.Setup(m => m.Get(itemForRepo.Id)).ReturnsAsync(itemForRepo);

        //Act
        GetItemDto result = await _itemService.Create(request);

        //Assert
        result.Id.Should().Be(itemForRepo.Id);
        result.Name.Should().Be(itemForRepo.Name);
        result.Price.Should().Be(itemForRepo.Price);
    }

    [Fact]
    public async Task Delete_GivenValidId_DoesntThrowException()
    {
        //Arrange
        int id = _fixture.Create<int>();
        string name = _fixture.Create<string>();
        decimal price = _fixture.Create<decimal>();

        _itemRepositoryMock.Setup(m => m.Get(id)).ReturnsAsync(new ItemEntity { Id = id, Name = name, Price = price });
        _itemRepositoryMock.Setup(m => m.Delete(id)).ReturnsAsync(1);

        //Act
        //Assert
        await _itemService.Invoking(r => r.Delete(id)).Should().NotThrowAsync<Exception>();
        _itemRepositoryMock.Verify(m => m.Delete(id), Times.Once);
    }

    [Fact]
    public async Task Delete_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        int id = _fixture.Create<int>();

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.Delete(id));
    }
    [Fact]
    public async void Delete_GivenInvalidId_DoesntCallRepository ()
    {
        //Arrange
        int id = _fixture.Create<int>();

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        _itemRepositoryMock.Verify(m => m.Delete(id), Times.Never);
    }

    [Fact]
    public async Task Edit_GivenValidId_ReturnsDto()
    {
        //Arrange
        int id = 1;
        string name = "Chocolate";
        string newName = "NewChocolate";
        decimal price = 1.99M;

        _itemRepositoryMock.Setup(m => m.Get(id)).ReturnsAsync(new ItemEntity { Id = id, Name = name, Price = price});
        _itemRepositoryMock.Setup(m => m.Edit(It.Is<ItemEntity>(i => i.Id == id && i.Name == newName && i.Price == price))).ReturnsAsync(id);

        //Act

        GetItemDto result = await _itemService.Edit(new PutItemDto {Id = id, Name = newName, Price = price});

        //Assert
        result.Name.Should().Be(newName);
    }

    [Fact]
    public async Task Edit_GivenInvalidId_ThrowsItemNotFoundException() 
    {
        //Arrange
        int id = _fixture.Create<int>();
        string name = _fixture.Create<string>();
        decimal price = _fixture.Create<decimal>();

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.Edit(new PutItemDto {Id = id, Name = name, Price = price }));
    }
}
