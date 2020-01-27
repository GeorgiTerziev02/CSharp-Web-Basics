namespace SIS.WebServer
{
    using SIS.HTTP.Common;
    using SIS.WebServer.Routing.Contracts;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            this.tcpListener = new TcpListener(IPAddress.Parse(LocalHostIpAddress), port);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started ont http://{LocalHostIpAddress}:{port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");

                var client = this.tcpListener.AcceptSocket();

                Task.Run(() => this.Listen(client));
            }
        }

        private async Task Listen(Socket client)
        {
            var conntectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
            await conntectionHandler.ProcessRequestAsync();
        }
    }
}
