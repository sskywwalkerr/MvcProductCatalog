using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _db;
    
    public ProductsController(AppDbContext db)
        {
        _db = db;
        }
    
    // Mixin
    private async Task PopulateCategoriesViewBagAsync(int? selectedCategoryId = null)
    {
        var categories = await _db.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCategoryId);
    }
    
    // GET: /Products/Index
    [HttpGet]
    public async Task<IActionResult> Index(int? categoryId)
    {
        var query = _db.Products.Include(p => p.Category) // left join что бы вернуть с категорией другие данные
            .AsQueryable(); // post init условия
        if (categoryId.HasValue)
        {
                query = query.Where(p => p.CategoryId == categoryId.Value); // lambda для фильтрации CategoryId
        }
        await PopulateCategoriesViewBagAsync(categoryId);
        
        var products = await query.OrderBy(p => p.Name).ToListAsync();
        return View(products);
    }
    
    // GET: /Products/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesViewBagAsync();
        return View();
    }
    
    // POST: /Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid) // Проверка на заполненность формы bool
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesViewBagAsync(product.CategoryId);
        return View(product);
    }
    
    // GET: /Products/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) 
            return NotFound();

        var product = await _db.Products.FindAsync(id);
        if (product == null) 
            return NotFound();

        await PopulateCategoriesViewBagAsync(product.CategoryId);
        return View(product);
    }
    
    // POST: /Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Product product)
    {
        if (id != product.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesViewBagAsync(product.CategoryId);
        return View(product);
    }

    // GET: /Products/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) 
            return NotFound();

        var product = await _db.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }
    
    // POST: /Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
