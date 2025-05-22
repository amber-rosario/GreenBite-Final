using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using GreenBite.Domain.Abstract;
using GreenBite.Domain.Entities;

namespace GreenBite.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ISaladRepository repository;
        // GET: Admin
        public AdminController(ISaladRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Salads);
        }

        public ViewResult Edit(int saladID)
        {
            Salad salad = repository.Salads
                .FirstOrDefault(g => g.SaladID == saladID);
            return View(salad);
        }

        [HttpPost]
        public ActionResult Edit(Salad salad, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    salad.ImageMimeType = Image.ContentType;
                    salad.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(salad.ImageData, 0, Image.ContentLength);
                }

                repository.SaveSalad(salad);
                TempData["message"] = $"{salad.Name} has been saved.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(salad);
            }
        }


        public ViewResult Create()
        {
            ViewBag.operation = "create";
            return View("Edit", new Salad());
        }

        [HttpPost]
        public ActionResult Delete(int saladID)
        {
            Salad deletedSalad = repository.DeleteSalad(saladID);
            if (deletedSalad != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedSalad.Name);
            }
            return RedirectToAction("Index");
        }
    }
}