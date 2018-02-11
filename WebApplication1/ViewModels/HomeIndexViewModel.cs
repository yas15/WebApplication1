using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeIndexViewModel
    {
        // let the HomePageViewModel class have a property of type IEnumerable<Restaurant>
        // i.e. a list of Restaurant type objects
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public string CurrentMessage { get; set; }

    }
}
