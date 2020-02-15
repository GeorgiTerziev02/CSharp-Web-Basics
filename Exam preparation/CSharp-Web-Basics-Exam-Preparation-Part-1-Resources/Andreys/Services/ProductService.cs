using Andreys.Data;
using Andreys.Models;
using Andreys.Models.Enums;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services
{
    public class ProductService : IProductService
    {
        private readonly AndreysDbContext db;

        public ProductService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void CreateProduct(ProductInputViewModel input)
        {
            var product = new Product
            {
                Name = input.Name,
                Price = input.Price,
                Category = Enum.Parse<Category>(input.Category),
                Description = input.Description,
                Gender = Enum.Parse<Gender>(input.Gender),
                ImageUrl = input.ImageUrl
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                this.db.Products.Remove(product);
                this.db.SaveChanges();
            }
        }

        public ProductDetailsViewModel GetProduct(int productId)
        {
            return this.db.Products.Where(x => x.Id == productId)
                .Select(x => new ProductDetailsViewModel
                {
                    Id = x.Id,
                    Category = x.Category.ToString(),
                    Price = x.Price,
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    ImageUrl = x.ImageUrl,
                    Name = x.Name
                }).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.db.Products.ToList();
        }
    }
}
