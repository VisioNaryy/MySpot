using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Domain.Data.Models;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Queries.UseCases.User.Get;
using MySpot.Infrastructure.Queries.UseCases.Users.Get;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Models;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.Auth.SignIn;
using MySpot.Services.UseCases.Auth.SignUp;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("users/[action]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpCommandHandler;
    private readonly ICommandHandler<SignIn> _signInCommandHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserQuery;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersQuery;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(
        ICommandHandler<SignUp> signUpCommandHandler,
        ICommandHandler<SignIn> signInCommandHandler,
        IQueryHandler<GetUser, UserDto> getUserQuery,
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersQuery,
        ITokenStorage tokenStorage)
    {
        _signUpCommandHandler = signUpCommandHandler;
        _signInCommandHandler = signInCommandHandler;
        _getUserQuery = getUserQuery;
        _getUsersQuery = getUsersQuery;
        _tokenStorage = tokenStorage;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUp command)
    {
        await _signUpCommandHandler.HandleAsync(command with {UserId = Guid.NewGuid()});

        return Ok();
    }

    [HttpPost]
    public async Task<JwtToken?> SignIn(SignIn command)
    {
        await _signInCommandHandler.HandleAsync(command);

        var jwt = _tokenStorage.Get();

        return jwt;
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet("me")]
    public async Task<IActionResult> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
            return NotFound();

        var userId = Guid.Parse(User.Identity?.Name);

        var user = await _getUserQuery.HandleAsync(new GetUser {UserId = userId});

        return Ok(user);
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _getUsersQuery.HandleAsync(new GetUsers());

        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var user = await _getUserQuery.HandleAsync(new GetUser {UserId = userId});

        return Ok(user);
    }
}