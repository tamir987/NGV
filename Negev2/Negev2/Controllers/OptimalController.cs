using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negev2.ViewModels;
using Negev2.Models;
using Negev2.DataContext;
using System.Net;
using System.Data.Entity;
using Negev2.DataContext.Repositories;

namespace Negev2.Controllers
{
    public class OptimalController : Controller
    {
        private ICropRepository dbCrop = new CropRepository();

        //Optimal constructor
        public OptimalController()
        {
           
        }

        // GET: OptimalIndex with viewModel of the database
        public ActionResult OptimalIndex()
        {
 
            OptimalViewModel optimalViewModel = new OptimalViewModel
            {
                Crops = dbCrop.GetMany(x => x.Quantity == 0),
                CurCrops = dbCrop.GetMany(x => x.Quantity > 0)
            };
            return View("OptimalIndex", optimalViewModel);
        }

        // GET: details of a specific crop
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Crops crop = dbCrop.GetById(newId);
            if (crop == null)
            {
                return HttpNotFound();
            }

            return View(crop);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Crops crop = dbCrop.GetById(newId);
            if (crop == null)
            {
                return HttpNotFound();
            }
            return View(crop);
        }

        // GET: Edit the quantity of a specific crop
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quantity")] Crops crop)
        {
            if (ModelState.IsValid)
            {
                var old = dbCrop.GetById(crop.Id);
                old.Quantity = crop.Quantity;
                dbCrop.Save();
                return RedirectToAction("OptimalIndex");
            }
            return View(crop);
        }

        // GET: delete a specific crop
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Crops crop = dbCrop.GetById(newId);
            if (crop == null)
            {
                return HttpNotFound();
            }
            return View(crop);
        }

        // GET: confirm the delete of a specific crop
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crops crop = dbCrop.GetById(id);
            crop.Quantity = 0;
            dbCrop.Save();
            return RedirectToAction("OptimalIndex");
        }

        // save a new crop and it's quantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(int id, int quantity)
        {
            var crop = dbCrop.GetById(id);
            crop.Quantity = quantity;
            dbCrop.Save();

            return RedirectToAction("OptimalIndex", "Optimal");
        }
    }
}