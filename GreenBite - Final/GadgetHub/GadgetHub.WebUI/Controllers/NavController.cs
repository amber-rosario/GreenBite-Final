using System.Web.Mvc;
using GreenBite.Domain.Entities; // or wherever your Salad entity is
using GreenBite.WebUI.Models;
using System.Linq;
using GreenBite.Domain.Concrete;
using System.Collections.Generic;

namespace GreenBite.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly EFDbContext context = new EFDbContext();

        public PartialViewResult Menu()
        {
            var salads = context.Salads.ToList();  // Make sure context is your EF DbContext
            return PartialView(salads);
        }


    }
}
