namespace Demo.App.Controllers
{
    using SIS.HTTP.Request.Contract;
    using SIS.HTTP.Responses.Contracts;


    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
}
