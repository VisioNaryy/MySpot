namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidCapacityException : CustomException
{
    public int Capacity { get; }

    public InvalidCapacityException(int capacity) 
        : base($"Capacity {capacity} is invalid.")
    {
        Capacity = capacity;
    }
}