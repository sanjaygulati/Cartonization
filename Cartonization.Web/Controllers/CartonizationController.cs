using Cartonization.Business;
using Cartonization.Web.Models;
using Cartonization.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cartonization.Web.Controllers
{
    public class CartonizationController : Controller
    {
        //
        // GET: /Cartonization/
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
        // GET: /Cartonization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cartonization/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cartonization/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cartonization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cartonization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cartonization/Delete/5
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            CartonizationService service = new CartonizationService();

            return Json(service.DeleteProduct(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(string sku, decimal h, decimal l, decimal w)
        {
            Product p = new Product(sku, h, l, w);

            CartonizationService service = new CartonizationService();

            return Json(service.AddProduct(p));
        }


        //
        // POST: /Cartonization/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
