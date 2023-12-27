using MultiLevelArchitectureProject.DAL.Interfaces;
using MultiLevelArchitectureProject.DAL.EF;
using MultiLevelArchitectureProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MultiLevelArchitectureProject.DAL.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private ApplicationContext db;
    private UserRepository? userRepository;
    private PurchaseRepository? purchaseRepository;
    public EFUnitOfWork(DbContextOptions<ApplicationContext> connectionString)
    {
        db = new ApplicationContext(connectionString);
    }
    public IRepository<User> Users
    {
        get
        {
            if (userRepository == null)
                userRepository = new UserRepository(db);
            return userRepository;
        }
    }
    public IRepository<Purchase> Purchases
    {
        get
        {
            if (purchaseRepository == null)
                purchaseRepository = new PurchaseRepository(db);
            return purchaseRepository;
        }
    }
    public void Save()
    {
        db.SaveChanges();
    }
    private bool disposed = false;
    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
            this.disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
