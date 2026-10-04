using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Укажите название товара")]
    [StringLength(150)]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Укажите цену товара")]
    [Range(0, 1000000, ErrorMessage = "Цена должна быть от 0 до 1 000 000")]
    [Column(TypeName = "decimal(18,2)")] // общее кол-во и после запятой
    [Display(Name = "Цена, руб.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Укажите артикул товара")]
    [StringLength(50)]
    [Display(Name = "Артикул SKU")]
    public string Sku { get; set; } = string.Empty;
    [Display(Name = "В наличии")] public bool InStock { get; set; } = true;
    
    [Display(Name = "Дата обновления")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "Выберите категорию")]
    [Display(Name = "Категория")]
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")] // Связь с Id
    [Display(Name = "Категория")]
    public Category? Category { get; set; }
}