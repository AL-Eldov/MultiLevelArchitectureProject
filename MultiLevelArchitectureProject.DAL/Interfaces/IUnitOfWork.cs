using MultiLevelArchitectureProject.DAL.Entities;

namespace MultiLevelArchitectureProject.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Purchase> Purchases { get; }
    void Save();
}
