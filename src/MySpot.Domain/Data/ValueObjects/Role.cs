using MySpot.Domain.Data.Enums;
using MySpot.Domain.Data.Exceptions;

namespace MySpot.Domain.Data.ValueObjects;

public sealed record Role
{
    public static IEnumerable<AvailableRole> AvailableRoles { get; } = new[] {AvailableRole.Admin, AvailableRole.User};

    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidRoleException(value);
        }

        if (Enum.TryParse(value, out AvailableRole role) is false)
        {
            throw new InvalidRoleException(value);
        }

        if (!AvailableRoles.Contains(role))
        {
            throw new InvalidRoleException(value);
        }

        Value = value;
    }

    public static Role Admin() => new("admin");

    public static Role User() => new("user");

    public static implicit operator Role(string value) => new (value);

    public static implicit operator string(Role value) => value.Value;

    public override string ToString() => Value;
}