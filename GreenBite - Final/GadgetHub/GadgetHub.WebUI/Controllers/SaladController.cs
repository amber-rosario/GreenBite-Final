using System.Linq;
using System.Web.Mvc;
using GreenBite.Domain.Abstract;
using GreenBite.Domain.Entities;
using GreenBite.WebUI.Models;

namespace GreenBite.WebUI.Controllers
{
    public class SaladController : Controller
    {
        private readonly ISaladRepository repository;

        public SaladController(ISaladRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            var salads = repository.Salads.OrderBy(s => s.SaladID);
            return View(new SaladListViewModel
            {
                Salads = salads
            });
        }


        public FileContentResult GetImage(int saladID)
        {
            Salad salad = repository.Salads.FirstOrDefault(s => s.SaladID == saladID);
            if (salad != null && salad.ImageData != null)
            {
                return File(salad.ImageData, salad.ImageMimeType);
            }
            return null;
        }


        public ActionResult Details(int id)
        {
            var salad = repository.GetSaladById(id); // get salad by id from your data source
            if (salad == null)
                return HttpNotFound();

            return View(salad);
        }


    }
}
