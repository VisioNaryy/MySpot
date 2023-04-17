using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.Auth.SignUp;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("users")]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpCommandHandler;

    public UsersController(ICommandHandler<SignUp> signUpCommandHandler)
    {
        _signUpCommandHandler = signUpCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SignUp command)
    {
        await _signUpCommandHandler.HandleAsync(command with { UserId = Guid.NewGuid()});

        return Ok();
    }
}