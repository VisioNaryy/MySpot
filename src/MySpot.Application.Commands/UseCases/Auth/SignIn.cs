namespace MySpot.Services.UseCases.Auth;

public record SignIn(string Email, string Password) : ICommand;
