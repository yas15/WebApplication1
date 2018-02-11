using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
