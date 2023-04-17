namespace MySpot.Services.UseCases.Auth.SignIn;

public record SignIn(string Email, string Password) : ICommand;
