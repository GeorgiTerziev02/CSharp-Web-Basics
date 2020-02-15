using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductInputViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                return this.Redirect("/Products/Add");
            }

            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Redirect("/Products/Add");
            }

            if (input.Description.Length > 10)
            {
                return this.Redirect("/Products/Add");
            }
            
            if (string.IsNullOrWhiteSpace(input.Category))
            {
                return this.Redirect("/Products/Add");
            }
            
            if (string.IsNullOrWhiteSpace(input.Gender))
            {
                return this.Redirect("/Products/Add");
            }   
            
            if (input.Price < 0)
            {
                return this.Redirect("/Products/Add");
            }

            productService.CreateProduct(input);

            return this.Redirect("/");
        }

        public HttpResponse Details(ProductIdViewModel productx)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = productService.GetProduct(productx.Id);
            if (product == null)
            {
                return this.Error("Not found");
            }

            return this.View(product);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            productService.Delete(id);

            return this.Redirect("/");
        }
    }
}
