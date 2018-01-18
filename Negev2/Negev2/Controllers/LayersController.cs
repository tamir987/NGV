using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Negev2.DataContext;
using Negev2.Models;
using Negev2.DataContext.Repositories;
using Negev2.HelpingModels;
using System.IO;
using Negev2.ViewModels;

namespace Negev2.Controllers
{
    public class LayersController : Controller
    {
        private LayerRepository db = new LayerRepository();

        // GET: Layers
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        public ActionResult MakeDB()
        {
            GeoJSONToDB makeMyDB = new GeoJSONToDB();
            makeMyDB.AddToDB(makeMyDB.GetMyJSON());
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult MakeGeoJson(String geoJsonToDB)
        //{
        //    GeoJSONToDB makeMyDB = new GeoJSONToDB();
        //    makeMyDB.AddToDB(geoJsonToDB);
        //    ViewBag.Message = ".התהליך עבר בהצלחה";
        //    return RedirectToAction("Index");
        //}

        // GET: Layers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Layer layer = db.GetById(newId);
            if (layer == null)
            {
                return HttpNotFound();
            }
            return View(layer);
        }

        // GET: Layers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Layers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Year")] Layer layer)
        {
            if (ModelState.IsValid)
            {
                db.Add(layer);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(layer);
        }

        // GET: Layers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Layer layer = db.GetById(newId);
            if (layer == null)
            {
                return HttpNotFound();
            }
            return View(layer);
        }

        // POST: Layers/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Year")] Layer layer)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(layer);
        }

        // GET: Layers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int newId = (int)id;
            Layer layer = db.GetById(newId);
            if (layer == null)
            {
                return HttpNotFound();
            }
            return View(layer);
        }

        // POST: Layers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int newId = (int)id;
            Layer layer = db.GetById(newId);
            db.Delete(layer);
            db.Save();
            return RedirectToAction("Index");
        }
    }
}