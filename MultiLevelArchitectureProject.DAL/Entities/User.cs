using System.ComponentModel.DataAnnotations;

namespace MultiLevelArchitectureProject.DAL.Entities;

public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Не указано имя")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Минимальная длина имени 3 символа")]
    public string? FullName { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime BornDate { get; set; }
}
