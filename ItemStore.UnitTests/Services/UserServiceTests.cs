﻿using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Profiles;
using ItemStore.WebApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        _jsonPlaceholderClientMock.Setup(m => m.GetUserAsync(user.Id)).ReturnsAsync(user);

        //Act
        GetUserDto result = await _userService.GetAsync(user.Id);

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<GetUserDto>(user));
    }

    // TODO AFTER ERROR HANDLING IMPLEMENTED:

    //[Theory]
    //[AutoData]
    //public async Task Get_GivenInvalidId_ThrowsItemNotFoundException(int id)
    //{
    //    //Arrange

    //    _itemRepositoryMock.Setup(m => m.Get(id)).Returns(Task.FromResult<ItemEntity>(null));

    //    //Act + Assert
    //    await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.Get(id));
    //}
}