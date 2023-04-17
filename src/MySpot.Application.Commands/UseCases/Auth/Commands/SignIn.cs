namespace MySpot.Services.UseCases.Auth.Commands;

public record SignIn(string Email, string Password) : ICommand;
