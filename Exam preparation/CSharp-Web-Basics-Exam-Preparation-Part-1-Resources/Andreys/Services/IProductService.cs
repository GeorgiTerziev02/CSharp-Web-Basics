using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        void CreateProduct(ViewModels.Products.ProductInputViewModel input);
        ProductDetailsViewModel GetProduct(int productId);
        void Delete(int id);
    }

}
