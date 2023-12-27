using System.ComponentModel.DataAnnotations;

namespace MultiLevelArchitectureProject.BLL.DTO;

public class PurchaseDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Пользователь не указан")]
    public int userId { get; set; }
    public UserDTO? user { get; set; }
    public float price { get; set; }
    public DateTime date { get; set; }
}
