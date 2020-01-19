namespace SIS.WebServer.Result
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Responses;

    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            :base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
