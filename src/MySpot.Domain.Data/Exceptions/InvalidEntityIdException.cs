namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidEntityIdException : BaseException
{
    public object Id { get; }

    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
        => Id = id;
}