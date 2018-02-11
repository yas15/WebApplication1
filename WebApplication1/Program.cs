using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Once we have an object of type IWebHost, that has been Created, Configured and Built.
            // We call the Run() method, to run the WebHost and start listening for HTTP connections.
            BuildWebHost(args).Run();
        }

        // This method returns an object of type IWebHost, taking as parameters
        // any arguments input into the command prompt

        // CreateDefaultBuilder() Initializes a new instance of the WebHostBuilder class.
        // UseStartup() instantiates an object of class 'Startup'. ASP.Net will invoke the 
        // 2 method in the 'Startup' class i.e. ConfigureServices() and the Configure() methods.

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
