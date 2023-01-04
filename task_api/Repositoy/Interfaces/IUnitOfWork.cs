namespace task_api.Repositoy.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartementRepository Departements { get; }
        Task SaveAsync();
    }
}
