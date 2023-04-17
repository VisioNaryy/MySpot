using MySpot.Data.EF.Repositories.Users.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.UseCases.Date.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Services.Exceptions;

namespace MySpot.Services.UseCases.Auth.SignUp;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(
        IUserRepository userRepository, 
        IPasswordManager passwordManager,
        IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(SignUp command)
    {
        var (guid, s, username1, password1, fullName1, role1) = command;
        
        var userId = new UserId(guid);
        var email = new Email(s);
        var username = new Username(username1);
        var password = new Password(password1);
        var fullName = new FullName(fullName1);
        var role = string.IsNullOrWhiteSpace(role1) ? Role.User() : new Role(role1);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = _passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, _clock.Current());
        await _userRepository.AddAsync(user);
    }
}