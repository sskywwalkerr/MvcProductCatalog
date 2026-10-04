using WebApplication1.Models;

namespace WebApplication1.Data;


public static class DbSeeder
{
    public static void Seed(AppDbContext db) // ничего не возвращать void
    {
        if (db.Categories.Any())
        {
            return;
        }
        
        var electronics = new Category { Name = "Электроника", Description = "Гаджеты и техника" };
        var home = new Category { Name = "Товары для дома", Description = "Всё для уюта" };
        var office = new Category { Name = "Канцтовары", Description = "Товары для офиса и учёбы" };
        
        db.Categories.AddRange(electronics, home, office); // добавить диапазон
        db.SaveChanges();
        
        db.Products.AddRange(
            new Product { Name = "Мышь", Description = "Беспроводная USB", Price = 1290, Sku = "EL-001", InStock = true, CategoryId = electronics.Id },
            new Product { Name = "Наушники", Description = "Bluetooth", Price = 3990, Sku = "EL-002", InStock = true, CategoryId = electronics.Id },
            new Product { Name = "Повербанк", Description = "10000 мАч", Price = 1590, Sku = "EL-003", InStock = false, CategoryId = electronics.Id },

            new Product { Name = "Кастрюли", Description = "Набор 3 шт", Price = 4590, Sku = "HM-001", InStock = true, CategoryId = home.Id },
            new Product { Name = "Плед", Description = "Флисовый 150x200", Price = 990, Sku = "HM-002", InStock = true, CategoryId = home.Id },

            new Product { Name = "Ежедневник", Description = "Формат А5", Price = 450, Sku = "OF-001", InStock = true, CategoryId = office.Id },
            new Product { Name = "Ручки", Description = "Гелевые 12 цветов", Price = 320, Sku = "OF-002", InStock = false, CategoryId = office.Id }
        );
        db.SaveChanges();
    }
}




