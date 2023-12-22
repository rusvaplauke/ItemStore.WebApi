using Moq;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Services;
using ItemStore.WebApi.Models.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.UnitTests.Services
{
    public class ItemServiceTests
    {
        [Fact]
        public async void Get_GivenValidId_ReturnsDto()
        {
            //Arrange
            int id = 1;
            ItemEntity item = new ItemEntity {Id = id, Name = "Chocolate", Price = 1.99M};

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Get(id)).ReturnsAsync(item);

            var itemService = new ItemService(testRepository.Object);

            //Act
            GetItemDto result = await itemService.Get(item.Id); 

            //Assert
            result.Id.Should().Be(item.Id);
            result.Name.Should().Be(item.Name);
            result.Price.Should().Be(item.Price);
        }

        [Fact]
        public async void Get_GivenInvalidId_ThrowsArgumentNullException()
        {
            //Arrange
            int id = 1;

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

            var itemService = new ItemService(testRepository.Object);

            //Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async() => await itemService.Get(id));
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

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Get()).ReturnsAsync(list);

            var itemService = new ItemService(testRepository.Object);

            //Act
            List<GetItemDto> result = await itemService.Get();

            //Assert
            result.Count().Should().Be(list.Count());
        }

        [Fact]
        public async void Create_GivenPosItemDto_ReturnsGetItemDto()
        {
            //Arrange
            PostItemDto request = new PostItemDto{Name = "Chocolate", Price = 1.99M};
            ItemEntity itemForRepo = new ItemEntity { Id = 1, Name = "Chocolate", Price = 1.99M}; // id sukuriamas automatiskai duombazej

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Create(It.IsAny<ItemEntity>())).ReturnsAsync(itemForRepo.Id); //is it bc we pass by reference?
            testRepository.Setup(m => m.Get(itemForRepo.Id)).ReturnsAsync(itemForRepo);

            var itemService = new ItemService(testRepository.Object);

            //Act
            GetItemDto result = await itemService.Create(request);

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

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Get(id)).ReturnsAsync(new ItemEntity { Id = 1, Name = "Chocolate", Price = 1.99M });
            testRepository.Setup(m => m.Delete(id)).ReturnsAsync(1);

            var itemService = new ItemService(testRepository.Object);

            //Act
            //Assert
            await itemService.Invoking(r => r.Delete(id)).Should().NotThrowAsync<Exception>();
            testRepository.Verify(m => m.Delete(id), Times.Once);
        }

        [Fact]
        public async void Delete_GivenInvalidId_ThrowsArgumentNullException()
        {
            //Arrange
            int id = 1;

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

            var itemService = new ItemService(testRepository.Object);

            //Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await itemService.Get(id));
        }

        [Fact]
        public async void Delete_IfNothingDoneInRepo_ThrowsException() 
        {
            //Arrange
            int id = 1;

            var testRepository = new Mock<IItemRepository>();
            testRepository.Setup(m => m.Delete(id)).Returns(Task.FromResult(0));

            var itemService = new ItemService(testRepository.Object);

            //Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await itemService.Delete(id));
        }
    }
}
