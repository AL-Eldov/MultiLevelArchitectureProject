using MultiLevelArchitectureProject.BLL.DTO;

namespace MultiLevelArchitectureProject.BLL.Interfaces;

public interface IApplicationService
{
    public PurchaseDTO GetPurchase(int? id);
    IEnumerable<PurchaseDTO> GetPurchases();       
    public void AddPurchase(PurchaseDTO purchase);
    public void RemovePurchase(int id);
    public void UpdatePurchase(PurchaseDTO purchase);
    public UserDTO GetUser(int? id);
    IEnumerable<UserDTO> GetUsers();
    public void AddUser(UserDTO user);
    public void RemoveUser(int id);
    public void UpdateUser(UserDTO user);
    public void Dispose();
}
