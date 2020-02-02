namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using SIS.HTTP.Response;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        protected HttpResponse View([CallerMemberName]string viewName = null)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            var html = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            var bodyWithLayouut = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayouut);
        }
    }
}
