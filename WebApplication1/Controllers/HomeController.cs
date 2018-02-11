using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    // The ASP.NET MVC framework maps URLs to classes and methods, 
    // these classes are referred to as controllers and methods are referred to as actions.
    // Define a new Controller (i.e a class in MVC) called HomeController, 
    // This Controller inherits from the AspNetCore.Mvc Controller class, 
    // The  Controller class gives us access to methods that respond to HTTP requests.
    public class HomeController : Controller
    {

        private IRestaurantData _restaurantData;
        private IGreeter _greeter;


        // constructor for the class HomeController i.e. the Controller 'Home' is instantiated 
        // when the middleware pipeline receives a http request for an action in thos controller.
        // This controlloer takes parameter of type IRestaurantData and IGreeter, these are
        // generated for us by the DI containers ConfigureServices() method of the Startup class
        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }



        // define an new action (a method in ASP.NET MVC) that returns an object that implements the IActionResult interface
        // localhost:54204/Home/Index
        public IActionResult Index()
        {

            var model = new HomeIndexViewModel();

            model.Restaurants = _restaurantData.GetAll();

            model.CurrentMessage = _greeter.GetMessageofTheDay();


            // The View() method returns an object of type ViewResult.
            // The View() method has 2 optional parameters i.e. a model and the name of a view here we are 
            // passing the model to View() method, which will in turn pass it to the corresponding viewresult.
            // if No view name parameter is specified for this method, the the runtime 
            // will by default look for the viewresult in the folders: - 
            // /Views/[The controller name]/Index.cshtml i.e. /Views/Home/Index.cshtml
            // or /Views/Shared/Index.cshtml i.e. /Views/Shared/Index.cshtml
            return View(model);
        }



        // define an new action (a method in ASP.NET MVC) that returns an object that implements the IActionResult interface
        // IActionResult methods typically take a model and returns a view.
        // Remember in MapRoute() method, in the ConfigureRoutes() method, we have defined the URL pattern 
        // as "{controller=Home}/{action=Index}/{id?}" Where {id?} is an optional parameter for the action.
        // localhost:54204/home/details/{id}
        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);

            // i.e. if _restaurantData.Get(id) return null
            if (model == null)
            {
                // if model id not found then redirect to the Index() action
                return RedirectToAction(nameof(Index));
            }


            // The Controller.View() method return an object of type ViewResult.
            // it can take an object (i.e. a model) and a 'ViewName' as parameters.
            // The returned ViewResult() object will render the specified view, if a ViewName is 
            // specified as a parameter. However if  the View() method is called with no parameters, 
            // MVC will look for a View with the name of the calling action method.
            // Remember that all ASP.Net MVC Views must be contained in the folder (/Views)
            // MVC will by default look for the viewresult in the folders: - 
            // {Views/controller_name/action_name.cshtml} i.e. /Views/Home/Details.cshtml
            // or {Views/Shared/action_name.cshtml} i.e. /Views/Shared/Details.cshtml
            return View(model);
        }


        public IActionResult Create(int id)
        {

            return View();
        }

        // @*The HTML<form> element defines a form that is used to collect user input.The attribute
        // method= "post", specifies that when the user clicks the submit button on the form,
        // the form will create a Http post message and post the form data back to the server.
        // We are not specifying the 'action' attribute of this tag i.e.the method to send and post
        // the form-data to when the form is submitted, this is because by default a form will alway
        // post back to the same URL that is came from, i.e.the same controller action. *@


        // Since this controller has 2 actions (i.e. ASP.NEt Mvc methods) with the name Create(),
        // We add the HttpGetAttribute to the controller.
        // The HttpGetAttribute specifies that an action supports a GET HTTP method only.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Since this controller has 2 actions (i.e. methods in ASP.Net MVC ) with the name Create(),
        // We add the HttpPostAttribute to the controller.
        // The HttpGetAttribute specifies that an action supports a POST HTTP method only.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // create a new object of type Restaurant called newRestaurant
                var newRestaurant = new Restaurant();

                // update the properties of the object newRestaurant
                newRestaurant.Cuisine = model.Cuisine;
                newRestaurant.Name = model.Name;

                newRestaurant = _restaurantData.Add(newRestaurant);


                // Post/Redirect/Get (or PRG) is a web development design pattern that helps to prevent 
                // duplicate form submissions (See notes on Post-Redirect-Get).
                // the RedirectToAction(action, parameter) method causes the browser to 
                // make a GET request to the specified action (i.e. "Details") with the specified id parameter.
                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }

            // The Controller.View() method return an object of type ViewResult.
            // it can take an object (i.e. a model) and a 'ViewName' as parameters.
            // The returned ViewResult() object will render the specified view, if a ViewName is 
            // specified as a parameter. However if  the View() method is called with no parameters, 
            // MVC will look for a View with the name of the calling action method.
            // Remember that all ASP.Net MVC Views must be contained in the folder (/Views)
            // MVC will by default look for the viewresult in the folders: - 
            // {Views/controller_name/action_name.cshtml} i.e. /Views/Home/Create.cshtml
            // or {Views/Shared/action_name.cshtml} i.e. /Views/Shared/Create.cshtml
            return View();
        }

    }
}
