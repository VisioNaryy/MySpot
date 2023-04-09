namespace MySpot.Domain.Data.Models;

public class ReservationDto
{
    public Guid Id { get; set;}
    public string EmployeeName { get; set; }
    public DateTime Date { get;  set; }
}