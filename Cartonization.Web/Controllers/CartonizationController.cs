using Cartonization.Business;
using Cartonization.Web.Models;
using Cartonization.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cartonization.Web.Controllers
{
    public class CartonizationController : Controller
    {
        //
        // GET: /Cartonization/
        [HttpGet]
        public ActionResult Index()
        {
            var service = new CartonizationService();
            CartonizationViewModel model = new CartonizationViewModel();

            model.AvailableCartons = service.GetAvailableCartons();
            model.ProductsToPack = service.GetProducts();
            model.Response = service.Pack();

            return View(model);
        }
        
        //
        // POST: /Cartonization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, decimal h, decimal l, decimal w)
        {
            CartonizationService service = new CartonizationService();
            List<Product> products = service.GetProducts();

            Product product = products.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                products.RemoveAll(p => p.ProductId == id);
                Product newProduct = new Product(id, h,l,w);
                products.Add(product);
                service.AddProduct(newProduct);
            }
            return Json(products);
        }

        //
        // GET: /Cartonization/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            CartonizationService service = new CartonizationService();

            return Json(service.DeleteProduct(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddProduct(DimensionsViewModel dimensions)
        {
            CartonizationService service = new CartonizationService();

            int productId = service.GetProducts().Count > 0 ? service.GetProducts().Last().ProductId + 1 : 1;

            Product p = new Product(productId, dimensions.Height, dimensions.Length, dimensions.Width);

            service.AddProduct(p);

            return Json(p);
        }

        [HttpPost]
        public ActionResult AddCarton(DimensionsViewModel dimensions)
        {
            CartonizationService service = new CartonizationService();

            int cartonId = service.GetAvailableCartons().Count > 0 ? service.GetAvailableCartons().Last().Id + 1 : 1;

            Carton p = new Carton(cartonId, dimensions.Height, dimensions.Length, dimensions.Width);

            service.AddCarton(p);

            return Json(p);
        }

        [HttpDelete]
        public ActionResult DeleteCarton(int id)
        {
            CartonizationService service = new CartonizationService();

            return Json(service.DeleteCarton(id), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Pack()
        {
            CartonizationService service = new CartonizationService();

            return PartialView("PackerResponse", service.Pack());
        }


    }
}
