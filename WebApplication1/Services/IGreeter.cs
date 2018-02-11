using Microsoft.Extensions.Configuration;

namespace WebApplication1.Services
{
    //define a new interface IGreeter
    public interface IGreeter
    {
        // the interface has a single methtod GetMessageofTheDay() that returns a string.
        string GetMessageofTheDay();
    }


    // define a new class called Greeter, that implements the IGreeter interface
    public class Greeter : IGreeter
    {

        private IConfiguration _configuration;

        // The Configuration API provides a way to configure an ASP.NET Core web app based on a list of name-value pairs.
        // The IConfiguration  interface is a special interface that the ASP.NET Core runtime knows how to implement
        // and so we do not need to provide a concrete type for this interface.
        // Configuration is read at runtime from multiple sources in  a hierarchical order. First from a json file 
        // called 'appsettings.json' (Only if you have add the file 'appsettings.json'), these values are overridden by 
        // EnvironmentVariables and EnvironmentVariables are overridden by command line parameters.
        public Greeter(IConfiguration configuration)
        {
            // In the constructor of this class set the value of _configuration to the configuration 
            _configuration = configuration;
        }


        public string GetMessageofTheDay()
        {
            // get the value of the key "Greeting" from the _configuration
            return _configuration["Greeting"];
        }

    }
}
