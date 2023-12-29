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
    private readonly Mock<IShopItemRepository> _shopItemRepositoryMock;
    private readonly Mock<IShopRepository> _shopRepositoryMock;
    private readonly ItemService _itemService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public ItemServiceTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _shopItemRepositoryMock = new Mock<IShopItemRepository>();
        _shopRepositoryMock = new Mock<IShopRepository>();

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<ItemProfile>()));

        _itemService = new ItemService(_itemRepositoryMock.Object, _mapper, _shopItemRepositoryMock.Object, _shopRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenValidId_ReturnsDto(int id, string name, decimal price)
    {
        //Arrange
        ItemEntity item = new ItemEntity {Id = id, Name = name, Price = price};

        _itemRepositoryMock.Setup(m => m.GetAsync(id)).ReturnsAsync(item);

        //Act
        GetItemDto result = await _itemService.GetAsync(item.Id);

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<GetItemDto>(item));
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException(int id)
    {
        //Arrange

        _itemRepositoryMock.Setup(m => m.GetAsync(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async() => await _itemService.GetAsync(id));
    }

    [Fact]
    public async Task Get_GivenNoId_ReturnsAllDtos()
    {
        //Arrange
        List<ItemEntity> list = new List<ItemEntity>();
        _fixture.AddManyTo(list,5);

        _itemRepositoryMock.Setup(m => m.GetAsync()).ReturnsAsync(list);

        //Act
        List<GetItemDto> result = await _itemService.GetAsync();

        //Assert
        result.Count().Should().Be(list.Count());
    }

    [Fact]
    public async Task Create_GivenPosItemDto_ReturnsGetItemDto()
    {
        //Arrange
        PostItemDto request = _fixture.Create<PostItemDto>();
        ItemEntity itemForRepo = _fixture.Build<ItemEntity>().With(x => x.Name, request.Name).With(x => x.Price, request.Price).Create();

        _itemRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<ItemEntity>())).ReturnsAsync(itemForRepo.Id); 

        _itemRepositoryMock.Setup(m => m.GetAsync(itemForRepo.Id)).ReturnsAsync(itemForRepo);

        //Act
        GetItemDto result = await _itemService.CreateAsync(request);

        //Assert
        result.Id.Should().Be(itemForRepo.Id);
        result.Name.Should().Be(itemForRepo.Name);
        result.Price.Should().Be(itemForRepo.Price);
    }

    [Theory]
    [AutoData]
    public async Task Delete_GivenValidId_DoesntThrowException(ItemEntity item)
    {
        _itemRepositoryMock.Setup(m => m.GetAsync(item.Id)).ReturnsAsync(new ItemEntity { Id = item.Id, Name = item.Name, Price = item.Price });
        _itemRepositoryMock.Setup(m => m.DeleteAsync(item.Id)).ReturnsAsync(1);

        //Act + Assert
        await _itemService.Invoking(r => r.DeleteAsync(item.Id)).Should().NotThrowAsync<Exception>();
        _itemRepositoryMock.Verify(m => m.DeleteAsync(item.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Delete_GivenInvalidId_ThrowsItemNotFoundException(int id)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.GetAsync(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.DeleteAsync(id));
    }
    
    [Theory]
    [AutoData]
    public async void Delete_GivenInvalidId_DoesntCallRepository (int id)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.GetAsync(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        _itemRepositoryMock.Verify(m => m.DeleteAsync(id), Times.Never);
    }

    [Fact] 
    public async Task Edit_GivenValidId_ReturnsDto()
    {
        //Arrange
        int id = 1;
        string name = "Chocolate";
        string newName = "NewChocolate";
        decimal price = 1.99M;

        ItemEntity originalItem = new ItemEntity { Id = id, Name = name, Price = price };
        ItemEntity changedItem = new ItemEntity {Id = id, Name = newName, Price = price};

        _itemRepositoryMock.Setup(m => m.GetAsync(id)).ReturnsAsync(originalItem);
        _itemRepositoryMock.Setup(m => m.EditAsync(It.Is<ItemEntity>(i => i.Id == id && i.Name == newName && i.Price == price)))
            .ReturnsAsync(changedItem);

        //Act

        GetItemDto result = await _itemService.EditAsync(new PutItemDto {Id = id, Name = newName, Price = price});

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<GetItemDto>(changedItem));
        _itemRepositoryMock.Verify(m => m.EditAsync(It.IsAny<ItemEntity>()), Times.Once);
    }

    [Fact] 
    public async Task Edit_GivenInvalidId_ThrowsItemNotFoundException() 
    {
        //Arrange
        int id = _fixture.Create<int>();
        string name = _fixture.Create<string>();
        decimal price = _fixture.Create<decimal>();

        _itemRepositoryMock.Setup(m => m.GetAsync(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.EditAsync(new PutItemDto {Id = id, Name = name, Price = price }));
    }
}
