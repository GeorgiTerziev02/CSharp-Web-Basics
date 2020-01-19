namespace SIS.HTTP.Request
{
    using SIS.HTTP.Common;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Headers.Contracts;
    using SIS.HTTP.Request.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, ISet<string>>();
            this.QueryData = new Dictionary<string, ISet<string>>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, ISet<string>> FormData { get; }

        public Dictionary<string, ISet<string>> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3
                || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParams)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

            return true;   
        }

        private void ParseRequestMethod(string[] requestLineParams)
        {
            HttpRequestMethod method;
            bool parseResult = HttpRequestMethod.TryParse(requestLineParams[0], true, out method);

            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage,
                    requestLineParams[0]));
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            this.Url = requestLineParams[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split(new[] { '?' })[0];
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            plainHeaders.Select(plainHeader => plainHeader
                                        .Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
                                        .ToList()
                                        .ForEach(headerKVP => this.Headers.AddHeader(new HttpHeader(headerKVP[0], headerKVP[1])));
        }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }

        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                var requestParametersPairs = this.Url.Split('?', '#')[1]
                    .Split('&')
                    .Select(plainQueryParameter => plainQueryParameter.Split('='))
                    .ToList();

                foreach (var pair in requestParametersPairs)
                {
                    var key = pair[0];
                    var value = pair[1];

                    if(!this.QueryData.ContainsKey(key))
                    {
                        this.QueryData.Add(key, new HashSet<string>());
                    }

                    this.QueryData[key].Add(WebUtility.UrlDecode(value));
                }
            }
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (!string.IsNullOrEmpty(requestBody))
            {
                var parametersPairs = requestBody
                    .Split('&')
                    .Select(plainQueryParameter => plainQueryParameter.Split('='))
                    .ToList();

                foreach (var pair in parametersPairs)
                {
                    string key = pair[0];
                    string value = pair[1];

                    if (!this.FormData.ContainsKey(key))
                    {
                        this.FormData.Add(key, new HashSet<string>());
                    }

                    this.FormData[key].Add(WebUtility.UrlDecode(value));
                }
            }
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
            //TODO: Split
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestString = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLineParams = splitRequestString[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestString).ToArray());
            //this.ParseCookies();

            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }

    }
}
