using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the ASP.NET core runtime. Use this method to add services to the service container i.e. our application.
        // Adding services to the service container makes them available within the app and in the Configure method. 
        // The services are resolved via dependency injection i.e. we specify for each interface type required by the
        // Configure() method, which concrete class shoudl we create an instance of.
        public void ConfigureServices(IServiceCollection services)
        {
            // to ConfigureServices add a service that implements the IGreeter interface
            // i.e. whenever we require an instance of an object that implements the IGreeter interface
            // create a instance of the class Greeter.
            // The returned object will be a Singleton i.e. the same object throughout the entire life of the process. 
            services.AddSingleton<IGreeter, Greeter>();

            services.AddDbContext<OdeToFoodDbContext>(options => 
            options.UseSqlServer(_configuration.GetConnectionString("OdeToFoodConnectionString")));

            // to the services add a service that implements the IRestaurantData interface
            // i.e. implements all the method of the interface IRestaurantData
            // whenever we require an instance of an object that implements the IRestaurantData interface
            // this method will (create and) return an instance of an SqlRestaurantData type object
            services.AddScoped<IRestaurantData, SqlRestaurantData>();


            // services.AddMvc() add a service to the ASP.Net core web application that 
            // tells the application to use MVC to handle any http requests
            services.AddMvc();

        }


        // This method gets called by the ASP.NET core runtime after the ConfigureServices() method has been called.
        // The Configure method is used to set up middleware
        // We Use the Configure() method to configure the HTTP request pipeline i.e. for every HTTP message that arrives,
        // this method defines how that HTTP request is handled.
        // This Configure method uses dependency injection, so for the parameters of this method the.net core runtime
        // will analyse the types of parameters required i.e. see if there are service or object that implement the inerfaces
        // required by the by the parameters of the configure method, and use those objects as the parameters of the method.

        // In the parameters of this method, we add all the services (i.e. classes that contain methods that our application 
        // will provide for the HTTP requests that our web appliaction recieves). 
        // We have manually added the parameter 'greeter' of type IGreeter
        // i.e. an interface (for the purposes of loose coupling), however the Configure() method needs a concrete 
        // implementation of this interfaces, this is where ConfigureServices() comes in.
        // ILogger Interface - A generic interface for logging.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IGreeter greeter, ILogger<Startup> logger)
        {
            // we set the environment variables in the json file 'launchSettings.json'
            if (env.IsDevelopment())
            {
                // this is a middleware component, all middleware componets begin with either a use..() or run...()
                // middleware components are extension methods on the IApplicationBuilder interface. 
                // all middleware components are called in the order it is placed in the Configure() method.
                // all middleware components have an optional parameter of type middleware_component_page_options.
                app.UseDeveloperExceptionPage();

            }



            // The UseStaticFiles() middleware allows the ASP.Net core application to serve default files.
            // i.e. it will search the wwwroot folder for following files: - 
            // index.html, index.htm, default.html, default.htm
            // UseDefaultFiles() doesn’t actually serve the file it is just an URL-rewriter 
            // to serve the files You need to also add UseStaticFiles().
            // Hence the reason why UseDefaultFiles() comes before UseStaticFiles() in the middleware pipeline.
            //app.UseDefaultFiles();


            // The UseStaticFiles() middleware allows the ASP.Net core application to serve Static files 
            // such as HTML, CSS, images, and JavaScript, that are stored in the wwwroot folder.
            // The files are served based on the http request path.
            app.UseStaticFiles();



            // UseMvc() Adds MVC middleware to the IApplicationBuilder request execution pipeline.
            // UseMvc() takes an Action delegate of type IRouteBuilder as a parameter. 
            // The IRouteBuilder parameter is used to configure the routing for MVC.
            // e.g. '{controller=Home}/{action=Index}/{id?}' - see "MVC routing" for more details.
            // We are now adding controllers and actions(i.e.ASP.Net MVC classes and methods) to the
            // Middleware pipeline. Each time the application receives a http request for a particular controller, 
            // the.Net core runtime will instantiate a new instance of that controller, additionally any dependencies 
            // the controller requires will be instantiated by the DI containers in the ConfigureServices() method.
            app.UseMvc(ConfigureRoutes);


            // app.Use() requires us to pass a function that takes a request delegate as a parameter and returns a request delegate.
            // A request delegate takes a HTTP context as a parameter and returns a task.
            // A HttpContext object will hold information about the current http request.
            // App.Run() requires us to pass a function that takes a request delegate as a parameter and returns a void
            // i.e. app.Use() may call the next middleware component in the pipeline app.Run will terminate the pipeline.
            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageofTheDay();


                context.Response.ContentType = "text/plain";


                // we have defined the value of the variable 'greeting' in the 'appsettings.json'
                // The value "env.EnvironmentName" comes from the 'launchSettings.json'
                await context.Response.WriteAsync($"Not Found");

            });

        }


        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // The ASP.NET MVC framework maps URLs to classes and methods, 
            // these classes are referred to as controllers and methods are referred to as actions.
            // Routing defines how an ASP.NET application maps a URI to an action. 
            // ASP.NET supports 2 types of routing, convention-based routing and attribute based routing.
            // Convention-based routing we use the method MapRoute() to map routes.
            // If a url has the following pattern url: "{controller}/{action}/{id}", then the 1st part is 
            // assumed to be the name of the controller the 2nd part the name of the action i.e the method
            // and the 3rd part {id} is a parameter that we can pass to the action i.e. the method
            // controller is a parameter with default value 'Home', action is a parameter with default value 'index'
            // id is an optional parameter [?] with no default value.
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }

    }
}
