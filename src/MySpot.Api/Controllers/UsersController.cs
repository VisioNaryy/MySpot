using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Queries.UseCases.User.Get;
using MySpot.Infrastructure.Queries.UseCases.Users.Get;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Models;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.Auth.SignIn;
using MySpot.Services.UseCases.Auth.SignUp;
using Swashbuckle.AspNetCore.Annotations;
using UserDto = MySpot.Domain.Data.Models.UserDto;

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
    [SwaggerOperation("Sign up a new user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        command = command with {UserId = Guid.NewGuid()};
        
        await _signUpCommandHandler.HandleAsync(command);

        return CreatedAtAction(nameof(GetAsync), new {command.UserId}, null);
    }

    [HttpPost]
    [SwaggerOperation("Sign in the existing user")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JwtToken?> SignInAsync(SignIn command)
    {
        await _signInCommandHandler.HandleAsync(command);

        var jwt = _tokenStorage.Get();

        return jwt;
    }
    
    [HttpGet("current")]
    [SwaggerOperation("Get full information about current user")]
    [Authorize(Policy = "is-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetInfoAsync()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
            return NotFound();

        var userId = Guid.Parse(User.Identity?.Name);

        var user = await _getUserQuery.HandleAsync(new GetUser {UserId = userId});

        return Ok(user);
    }


    [HttpGet]
    [SwaggerOperation("Get list of all users")]
    [Authorize(Policy = "is-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetAllAsync([FromQuery] GetUsers query)
    {
        var users = await _getUsersQuery.HandleAsync(query);

        return Ok(users);
    }
    
    [HttpGet("{userId:guid}")]
    [SwaggerOperation("Get individual user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetAsync(Guid userId)
    {
        var user = await _getUserQuery.HandleAsync(new GetUser {UserId = userId});

        if (user is null)
            return NotFound();

        return user;
    }
}