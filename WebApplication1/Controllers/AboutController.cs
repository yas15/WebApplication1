using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    // The ASP.NET MVC framework maps URLs to classes and methods, 
    // these classes are referred to as controllers and methods are referred to as actions.
    // Routing defines how an ASP.NET application maps a URI to an action. 
    // ASP.NET supports 2 types of routing, convention-based routing and attribute based routing.
    // With attribute-based routing we specify the URL pattern in an attribute.
    // To reach this controller i.e class the first part of the url must contain the string "about".
    [Route("about")]
    public class AboutController
    {

        // To reach this controller the second part of the url must be ""
        // localhost:54204/About/
        [Route("")]
        public string Phone()
        {
            return "1+555-555-5555";
        }

        // To reach this controller the second part of the url must be "Address".
        // localhost:54204/About/Address
        [Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}
