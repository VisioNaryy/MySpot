namespace MySpot.Services.UseCases.Auth.Implementation;

public record SignIn(string Email, string Password) : ICommand;
