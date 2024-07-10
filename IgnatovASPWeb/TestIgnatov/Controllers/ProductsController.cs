using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestIgnatov.Data;
using TestIgnatov.Models;
using TestIgnatov.Models.ViewModels;

namespace TestIgnatov.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsDbContext _productsDbContext;
        public ProductsController(ProductsDbContext productsDbContext)
        {
            _productsDbContext = productsDbContext;
        }
        [HttpGet]
        public IActionResult Products()
        {
            List<Product> products = _productsDbContext.Products.ToList();

            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProduct userProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Name = userProduct.Name,
                    Price = userProduct.Price,
                    Stock = userProduct.Stock,
                };
                await _productsDbContext.Products.AddAsync(product);
                await _productsDbContext.SaveChangesAsync();
                return RedirectToAction("Products", "Products");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Product product = _productsDbContext.Products.Find(id);

            EditProduct editProduct = new EditProduct();
            editProduct.Id =  product.Id;
            editProduct.Name =  product.Name;
            editProduct.Price =  Math.Round(product.Price, 2);
            editProduct.Stock =  product.Stock;
            return View(editProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProduct editProduct)
        {
            Product product = new Product(editProduct.Id, editProduct.Name, editProduct.Price, editProduct.Stock);
            
            _productsDbContext.Products.Update(product);
            await _productsDbContext.SaveChangesAsync();
            return RedirectToAction("Products", "Products");
        }
        public async Task<IActionResult> Delete(string id)
        {
            Product product = _productsDbContext.Products.Find(id);

            _productsDbContext.Products.Remove(product);
            await _productsDbContext.SaveChangesAsync();
            return RedirectToAction("Products", "Products");
        }
    }
}