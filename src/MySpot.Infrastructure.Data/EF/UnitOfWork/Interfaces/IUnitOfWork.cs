namespace MySpot.Data.EF.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    Task ExecuteInTransactionAsync(Func<Task> action);
}