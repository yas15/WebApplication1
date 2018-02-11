using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SqlRestaurantData : IRestaurantData
    {

        private OdeToFoodDbContext _context;


        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }


        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            _context.SaveChanges();
            return newRestaurant;
        }


        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }
        

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }

    }
}
