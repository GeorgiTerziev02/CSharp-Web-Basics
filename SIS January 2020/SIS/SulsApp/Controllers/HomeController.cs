namespace SulsApp.Controllers
{
    using SIS.HTTP;
    using SIS.HTTP.Response;
    using System.IO;

    public class HomeController
    {
        public HttpResponse Index(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Home/Index.html");
            var bodyWithLayouut = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayouut);
        }
    }
}
