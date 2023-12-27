using AutoMapper;
using MultiLevelArchitectureProject.BLL.DTO;
using MultiLevelArchitectureProject.BLL.Interfaces;
using MultiLevelArchitectureProject.DAL.Entities;
using MultiLevelArchitectureProject.DAL.Interfaces;

namespace MultiLevelArchitectureProject.BLL.Services;

public class ApplicationService : IApplicationService
{
    IUnitOfWork Database { get; set; }
    public ApplicationService(IUnitOfWork uow)
    {
        Database = uow;
    }
    public UserDTO GetUser(int? id)
    {
        if (id == null)
            throw new Exception("Не установлено id пользователя");
        var user = Database.Users.Get(id.Value);
        return new UserDTO { Id = user.Id, FullName = user.FullName, BornDate = user.BornDate };
    }
    public IEnumerable<UserDTO> GetUsers()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
    }
    public void AddUser(UserDTO userDTO)
    {
        Database.Users.Create(new User { Id = userDTO.Id, FullName = userDTO.FullName, BornDate = userDTO.BornDate });
        Database.Save();
    }
    public void RemoveUser(int id)
    {
        Database.Users.Delete(id);
        Database.Save();
    }
    public void UpdateUser(UserDTO userDTO)
    {
        Database.Users.Update(new User { Id = userDTO.Id, FullName = userDTO.FullName, BornDate = userDTO.BornDate });
        Database.Save();
    }
    public PurchaseDTO GetPurchase(int? id)
    {
        if (id == null)
            throw new Exception("Не установлено id заказа");
        var purchase = Database.Purchases.Get(id.Value);
        return new PurchaseDTO { Id = purchase.Id, userId = purchase.userId, price = purchase.price, date = purchase.date };
    }
    public IEnumerable<PurchaseDTO> GetPurchases()
    {
        IEnumerable<Purchase> purchases = Database.Purchases.GetAll();
        List<PurchaseDTO> purchaseDTOs = new List<PurchaseDTO>();
        foreach (var purchase in purchases)
        {
            if (purchase.user is not null)
            {
                UserDTO userDTO = new UserDTO { Id = purchase.user.Id, FullName = purchase.user.FullName, BornDate = purchase.user.BornDate };
                purchaseDTOs.Add(new PurchaseDTO { Id = purchase.Id, userId = purchase.userId, price = purchase.price, date = purchase.date, user = userDTO });
            }
        }
        return purchaseDTOs;
    }
    public void AddPurchase(PurchaseDTO purchaseDTO)
    {
        User tempUser = Database.Users.Get(purchaseDTO.userId);
        Database.Purchases.Create(new Purchase { Id = purchaseDTO.Id, userId = purchaseDTO.userId, price = purchaseDTO.price, date = purchaseDTO.date, user = tempUser });
        Database.Save();
    }
    public void RemovePurchase(int id)
    {
        Database.Purchases.Delete(id);
        Database.Save();
    }
    public void UpdatePurchase(PurchaseDTO purchaseDTO)
    {
        User tempUser = Database.Users.Get(purchaseDTO.userId);
        Database.Purchases.Update(new Purchase { Id = purchaseDTO.Id, userId = purchaseDTO.userId, price = purchaseDTO.price, date = purchaseDTO.date, user = tempUser });
        Database.Save();
    }
    public void Dispose()
    {
        Database.Dispose();
    }
}
