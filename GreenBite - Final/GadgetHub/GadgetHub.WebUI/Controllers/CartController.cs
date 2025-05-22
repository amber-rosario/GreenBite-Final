using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using GreenBite.Domain.Abstract;
using GreenBite.Domain.Entities;
using GreenBite.WebUI.Models;

namespace GreenBite.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISaladRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(ISaladRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int saladID, string returnUrl)
        {
            Salad salad = repository.Salads.FirstOrDefault
                                              (g => g.SaladID == saladID);

            if (salad != null)
            {
                GetCart().AddItem(salad, 1);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int saladID, string returnUrl)
        {
            Salad salad = repository.Salads.FirstOrDefault
                                                 (g => g.SaladID == saladID);
            if (salad != null)
            {
                GetCart().RemoveLine(salad);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl

            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}