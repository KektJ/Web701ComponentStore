using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace StoreSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {

            // server config
            Uri _baseAddress = new Uri("http://localhost:8555/");
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );
            // Create server
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            // Start listening
            server.OpenAsync().Wait();
            Console.WriteLine("Component Store API hosted on:" + _baseAddress);
            Console.WriteLine("Press Enter to close the service");
            Console.ReadLine();
            server.CloseAsync().Wait();

        }
    }
}
