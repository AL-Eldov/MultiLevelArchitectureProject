using Microsoft.EntityFrameworkCore;
using MultiLevelArchitectureProject.DAL.EF;
using MultiLevelArchitectureProject.DAL.Entities;
using MultiLevelArchitectureProject.DAL.Interfaces;

namespace MultiLevelArchitectureProject.DAL.Repositories;

internal class PurchaseRepository : IRepository<Purchase>
{
    private ApplicationContext db;
    public PurchaseRepository(ApplicationContext db)
    {
        this.db = db;
    }
    public IEnumerable<Purchase> GetAll()
    {
        return db.Purchases.Include(p => p.user);
    }
    public Purchase Get(int id)
    {
        return db.Purchases.Find(id)!;
    }
    public void Create(Purchase purchase)
    {
        db.Purchases.Add(purchase);
    }
    public void Update(Purchase purchase)
    {
        db.Purchases.Update(purchase);
    }
    public void Delete(int id)
    {
        Purchase purchase = db.Purchases.Find(id)!;
        if (purchase != null)
            db.Purchases.Remove(purchase);
    }
}
