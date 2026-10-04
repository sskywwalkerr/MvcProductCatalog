using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Category
{   
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Для продолжения укажите название категории")]
    [StringLength(100)]
    [Display(Name = "Название категории")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Описание категории")]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>(); // коллекция товаров один ко многим
}