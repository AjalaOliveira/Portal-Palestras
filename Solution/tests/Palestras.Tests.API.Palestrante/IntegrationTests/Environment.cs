using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Palestras.WebApi;
using System.Net.Http;

namespace Palestras.Tests.API.Palestrante.IntegrationTests
{
    public class Environment
    {
        public static TestServer Server;
        public static HttpClient Client;

        public static void CreateServer()
        {
            Server = new TestServer(
                    new WebHostBuilder()
                        .UseEnvironment("Development")
                        .UseUrls("http://localhost:8285")
                        .UseStartup<StartUpTests>()
                );

            Client = Server.CreateClient();
        }
    }
}
