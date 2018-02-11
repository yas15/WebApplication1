using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AboutAController
    {
        // localhost:54204/AboutA/phone
        public string Phone()
        {
            return "1+555-777-9999";
        }

        // localhost:54204/AboutA/Address
        public string Address()
        {
            return "USA-A";
        }
    }
}
