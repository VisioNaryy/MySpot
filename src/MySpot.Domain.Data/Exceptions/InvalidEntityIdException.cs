namespace MySpot.Comain.Data.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public object Id { get; }

    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
        => Id = id;
}