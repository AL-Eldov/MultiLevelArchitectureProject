using MultiLevelArchitectureProject.DAL.EF;
using MultiLevelArchitectureProject.DAL.Interfaces;
using MultiLevelArchitectureProject.DAL.Entities;

namespace MultiLevelArchitectureProject.DAL.Repositories;

public class UserRepository : IRepository<User>
{
    private ApplicationContext db;
    public UserRepository(ApplicationContext db)
    {
        this.db = db;
    }
    public IEnumerable<User>  GetAll() 
    {
        return db.Users;
    }
    public User Get(int id) 
    {
        return db.Users.Find(id)!;
    }
    public void Create(User user) 
    {
        db.Users.Add(user);
    }
    public void Update(User user) 
    {
        db.Users.Update(user);           
    }
    public void Delete(int id) 
    {
        User user = db.Users.Find(id)!;
        if (user != null) 
            db.Users.Remove(user);
    }
}
