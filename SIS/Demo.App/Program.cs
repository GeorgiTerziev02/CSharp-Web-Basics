using SIS.HTTP.Request;
using System;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = 
                "POST /url/asd?name=john&id=1#fragment HTTP/1.1\r\n" +
                "Authorization: Basic 312312312\r\n" +
                "Date: " +DateTime.Now + "\r\n" +
                "Host: localhost:5000\r\n" +
                "\r\n" +
                "username=johndoe&password=123";

            HttpRequest httpRequest = new HttpRequest(request);

            var a = 5;
        }
    }
}
