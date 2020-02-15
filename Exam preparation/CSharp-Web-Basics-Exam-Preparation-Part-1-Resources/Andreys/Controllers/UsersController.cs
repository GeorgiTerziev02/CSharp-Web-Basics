using Andreys.Services;
using Andreys.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public HttpResponse Register()
        {
            return this.View("Register");
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Username)||input.Username.Length < 4 || input.Username.Length > 10)
            {
                return this.Error("/Users/Register");
            }

            if (string.IsNullOrWhiteSpace(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("/Users/Register");
            }

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return this.Error("/Users/Register");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("/Users/Register");
            }

            this.userService.Create(input);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputViewModel input)
        {
            var userId = this.userService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }
    }
}
