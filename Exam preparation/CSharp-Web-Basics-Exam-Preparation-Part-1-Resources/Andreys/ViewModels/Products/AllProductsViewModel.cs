using Andreys.Models;
using System.Collections.Generic;

namespace Andreys.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public IEnumerable<ProductInfoViewModel> Products { get; set; }
    }
}
