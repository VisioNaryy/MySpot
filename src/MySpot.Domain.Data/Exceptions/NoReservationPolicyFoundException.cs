using MySpot.Comain.Data.ValueObjects;

namespace MySpot.Comain.Data.Exceptions;

public sealed class NoReservationPolicyFoundException : CustomException
{
    public JobTitle JobTitle { get; }

    public NoReservationPolicyFoundException(JobTitle jobTitle) 
        : base($"No reservation policy for {jobTitle} found")
    {
        JobTitle = jobTitle;
    }
}