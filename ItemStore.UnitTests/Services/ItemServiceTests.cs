using Moq;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Services;
using ItemStore.WebApi.Models.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ItemStore.WebApi.Profiles;
using ItemStore.WebApi.Exceptions;

namespace ItemStore.UnitTests.Services;

public class ItemServiceTests
{
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;
    private readonly IMapper _mapper;

    public ItemServiceTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<ItemProfile>()));

        _itemService = new ItemService(_itemRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async void Get_GivenValidId_ReturnsDto()
    {
        //Arrange
        int id = 1;
        ItemEntity item = new ItemEntity {Id = id, Name = "Chocolate", Price = 1.99M};

        _itemRepositoryMock.Setup(m => m.Get(id)).ReturnsAsync(item);

        //Act
        GetItemDto result = await _itemService.Get(item.Id); 

        //Assert
        result.Id.Should().Be(item.Id);
        result.Name.Should().Be(item.Name);
        result.Price.Should().Be(item.Price);
    }

    [Fact]
    public async void Get_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        int id = 1;

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async() => await _itemService.Get(id));
    }

    [Fact]
    public async void Get_GivenNoId_ReturnsAllDtos()
    {
        //Arrange
        List<ItemEntity> list = new List<ItemEntity> {
            new ItemEntity() { Id = 1, Name = "Chocolate", Price = 1.99M },
            new ItemEntity() { Id = 2, Name = "Chocolate 2", Price = 1.99M },
            new ItemEntity() { Id = 3, Name = "Chocolate 3", Price = 1.99M }
        };

        _itemRepositoryMock.Setup(m => m.Get()).ReturnsAsync(list);

        //Act
        List<GetItemDto> result = await _itemService.Get();

        //Assert
        result.Count().Should().Be(list.Count());
    }

    [Fact]
    public async void Create_GivenPosItemDto_ReturnsGetItemDto()
    {
        // this can be generated with autofixture
        //Arrange
        PostItemDto request = new PostItemDto{Name = "Chocolate", Price = 1.99M};
        ItemEntity itemForRepo = new ItemEntity { Id = 1, Name = "Chocolate", Price = 1.99M};

        _itemRepositoryMock.Setup(m => m.Create(It.IsAny<ItemEntity>())).ReturnsAsync(itemForRepo.Id); //is it bc we pass by reference?
        //                                  It.IsAny<ItemEntity>(x => x.Name == itemDto.Name ) <-- nurodome sitoj vietoj konkretu pvz, su kuriom saukiam. Pagalvot, kodel tai geriau

        _itemRepositoryMock.Setup(m => m.Get(itemForRepo.Id)).ReturnsAsync(itemForRepo);

        //Act
        GetItemDto result = await _itemService.Create(request);

        //Assert
        result.Id.Should().Be(itemForRepo.Id);
        result.Name.Should().Be(itemForRepo.Name);
        result.Price.Should().Be(itemForRepo.Price);
    }

    [Fact]
    public async void Delete_GivenValidId_DoesntThrowException()
    {
        //Arrange
        int id = 1;

        _itemRepositoryMock.Setup(m => m.Get(id)).ReturnsAsync(new ItemEntity { Id = 1, Name = "Chocolate", Price = 1.99M });
        _itemRepositoryMock.Setup(m => m.Delete(id)).ReturnsAsync(1);

        //Act
        //Assert
        await _itemService.Invoking(r => r.Delete(id)).Should().NotThrowAsync<Exception>();
        _itemRepositoryMock.Verify(m => m.Delete(id), Times.Once);
    }

    [Fact]
    public async void Delete_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        int id = 1;

        _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

        //Act + Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.Get(id));
    }

    [Fact]
    public async void Edit_GivenValidId_ReturnsDto()
    { 
        //
    }

    [Fact]
    public async void Edit_GivenInvalidId_ThrowsArgumentNullException() { }
}
