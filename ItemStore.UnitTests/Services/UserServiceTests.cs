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
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public UserServiceTests()
    {
        _jsonPlaceholderClientMock = new Mock<IJsonPlaceholderClient>();

        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<UserProfile>()));

        _userService = new UserService(_jsonPlaceholderClientMock.Object, _mapper);
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenValidId_ReturnsDto(UserEntity user)
    {
        //Arrange
        _jsonPlaceholderClientMock.Setup(m => m.GetUserAsync(user.Id))
                .ReturnsAsync(new JsonPlaceholderResult<UserEntity> { DataItem = user, IsSuccessful = true });

        //Act
        GetUserDto result = await _userService.GetAsync(user.Id);

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<GetUserDto>(user));
    }

    [Theory]
    [AutoData]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException(int id)
    {
        //Arrange
        _jsonPlaceholderClientMock.Setup(m => m.GetUserAsync(id))
            .ReturnsAsync(new JsonPlaceholderResult<UserEntity> {IsSuccessful = false});

        //Act + Assert
        await Assert.ThrowsAsync<JsonPlaceholderException>(async () => await _userService.GetAsync(id));
    }
}
