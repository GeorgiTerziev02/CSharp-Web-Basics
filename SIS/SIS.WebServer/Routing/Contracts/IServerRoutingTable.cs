namespace SIS.WebServer.Routing.Contracts
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Request.Contract;
    using SIS.HTTP.Responses.Contracts;
    using System;

    public interface IServerRoutingTable
    {
        void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

        bool Contains(HttpRequestMethod method, string path);

        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path);
    }
}
