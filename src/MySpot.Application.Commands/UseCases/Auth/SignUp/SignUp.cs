namespace MySpot.Services.UseCases.Auth.SignUp;

public record SignUp(Guid UserId, string Email, string Username, string Password, string FullName, string Role) : ICommand;
