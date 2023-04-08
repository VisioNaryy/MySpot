using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Entities;

public class User
{
    public UserId Id { get; }
    public Email Email { get; }
    public Username Username { get; }
    public Password Password { get; }
    public FullName FullName { get; }
    public Role Role { get; }
    public DateTime CreatedAt { get; }

    public User(UserId id, Email email, Username username, Password password, FullName fullName, Role role,
        DateTime createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        FullName = fullName;
        Role = role;
        CreatedAt = createdAt;
    }
}