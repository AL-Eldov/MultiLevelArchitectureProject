using System.ComponentModel.DataAnnotations;

namespace MultiLevelArchitectureProject.DAL.Entities;

public class Purchase
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Пользователь не указан")]
    public int userId { get; set; }
    public User? user { get; set; }
    [Required(ErrorMessage = "Цена не указана")]
    public float price { get; set; }
    public DateTime date { get; set; }
}
