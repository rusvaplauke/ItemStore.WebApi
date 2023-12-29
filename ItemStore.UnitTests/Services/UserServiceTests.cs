using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Profiles;
using ItemStore.WebApi.Services;
using Moq;

namespace ItemStore.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IJsonPlaceholderClient> _jsonPlaceholderClientMock;
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public UserServiceTests()
    {
        _jsonPlaceholderClientMock = new Mock<IJsonPlaceholderClient>();
        _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        _itemRepositoryMock = new Mock<IItemRepository>();

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<UserProfile>()));

        _userService = new UserService(_jsonPlaceholderClientMock.Object, _itemRepositoryMock.Object, _purchaseRepositoryMock.Object, _mapper);
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenValidId_ReturnsDto(UserEntity user)
    {
        //Arrange
        JsonPlaceholderResult<UserEntity> response = new JsonPlaceholderResult<UserEntity> {IsSuccessful = true, DataItem = user};
        _jsonPlaceholderClientMock.Setup(m => m.GetAsync(user.Id)).ReturnsAsync(response);

        //Act
        GetUserDto result = await _userService.GetAsync(response.DataItem.Id);

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<GetUserDto>(user));
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException(int id)
    {
        //Arrange
        JsonPlaceholderResult<UserEntity> response = new JsonPlaceholderResult<UserEntity> { IsSuccessful = false, ErrorMessage = ""};
        _jsonPlaceholderClientMock.Setup(m => m.GetAsync(id)).ReturnsAsync(response);

        //Act + Assert
        await Assert.ThrowsAsync<JsonPlaceholderException>(async () => await _userService.GetAsync(id));
        _jsonPlaceholderClientMock.Verify(m => m.GetAsync(id), Times.Once);
    }
}
