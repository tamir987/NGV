using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Negev2.DataContext;
using Negev2.Models;
using Negev2.DataContext.Repositories;
using Negev2.DataContext.Infrastructure;
using Negev2.ViewModels;
using Negev2.HelpingModels;

namespace Negev2.Controllers
{
    public class CropsController : Controller
    {
  
        private ICropRepository db = new CropRepository();


        public CropsController()
        {

        }

        // GET: Crops
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Crops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Crops crops = db.GetById(newId);
            if (crops == null)
            {
                return HttpNotFound();
            }
            return View(crops);
        }

        // GET: Crops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crops/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Crops crops)
        {
            if (ModelState.IsValid)
            {
                db.Add(crops);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(crops);
        }

        // GET: Crops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Crops crops = db.GetById(newId);
            if (crops == null)
            {
                return HttpNotFound();
            }
            return View(crops);
        }

        // POST: Crops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Crops crops)
        {
            if (ModelState.IsValid)
            {
                var old = db.GetById(crops.Id);
                old.Name = crops.Name;
                db.Save();
                return RedirectToAction("Index");
            }
            return View(crops);
        }

        // GET: Crops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int newId = (int)id;
            Crops crops = db.GetById(newId);
            if (crops == null)
            {
                return HttpNotFound();
            }
            return View(crops);
        }

        // POST: Crops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crops crops = db.GetById(id);
            db.Delete(crops);
            db.Save();
            return RedirectToAction("Index");
        }
    }
}
