using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Exceptions;

public sealed class NoReservationPolicyFoundException : BaseException
{
    public JobTitle JobTitle { get; }

    public NoReservationPolicyFoundException(JobTitle jobTitle) 
        : base($"No reservation policy for {jobTitle} found")
    {
        JobTitle = jobTitle;
    }
}