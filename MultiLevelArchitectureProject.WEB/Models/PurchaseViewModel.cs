using System.ComponentModel.DataAnnotations;


namespace MultiLevelArchitectureProject.WEB.Models;

public class PurchaseViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Пользователь не указан")]
    public int userId { get; set; }
    public UserViewModel? user { get; set; }
    [Required(ErrorMessage = "Цена не указана")]
    public float price { get; set; }
    [Required(ErrorMessage = "Дата не указана")]
    public DateTime date { get; set; }
}
