using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Data.EF.Repositories.Users.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.Models;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.UseCases.Date.Implementation;
using MySpot.Infrastructure.Services.UseCases.Security.Models;
using MySpot.Services.UseCases.Auth.SignIn;
using MySpot.Services.UseCases.Auth.SignUp;
using MySpot.Infrastructure.Services.UseCases.Security.Implementation;

namespace MySpot.Tests.Integrational.Controllers;

public class UsersControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task post_users_should_return_created_201_status_code()
    {
        await _testDatabase.Context.Database.MigrateAsync();
        var command = new SignUp(Guid.Empty, "test-user1@myspot.io", "test-user1", "secret",
            "Test Doe", "user");
        var response = await Client.PostAsJsonAsync("users", command);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task post_sign_in_should_return_ok_200_status_code_and_jwt()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        
        var user = new User(Guid.NewGuid(), "test-user1@myspot.io",
            "test-user1", passwordManager.Secure(password), "Test Doe", Role.User(), clock.Current());
        await _userRepository.AddAsync(user);
        // await _testDatabase.Context.Database.MigrateAsync();
        // await _testDatabase.Context.Users.AddAsync(user);
        // await _testDatabase.Context.SaveChangesAsync();

        // Act
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("users/sign-in", command);
        var jwt = await response.Content.ReadFromJsonAsync<JwtToken>();

        // Assert
        jwt.Should().NotBeNull();
        jwt.AccessToken.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        
        var user = new User(Guid.NewGuid(), "test-user1@myspot.io",
            "test-user1", passwordManager.Secure(password), "Test Doe", Role.User(), clock.Current());
        await _testDatabase.Context.Database.MigrateAsync();
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();

        // Act
        Authorize(user.Id, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");

        // Assert
        userDto.Should().NotBeNull();
        userDto.Id.Should().Be(user.Id.Value);
    }

    private IUserRepository _userRepository;
    private readonly TestDatabase _testDatabase;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        _userRepository = new TestUserRepository();
        services.AddSingleton(_userRepository);
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}