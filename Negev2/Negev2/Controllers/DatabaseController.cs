using Negev2.Chaka;
using Negev2.HelpingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Negev2.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetJson()
        {
            //LayeredGeoJson lay = new LayeredGeoJson();
            //String geoJson = lay.GetGeoJsonByLayer(1);
            //string path = Server.MapPath("~/App_Data/");
            // Write that JSON to txt file,  
            //System.IO.File.WriteAllText(path + "output.geojson", geoJson);
            DBToGeoJson vax = new DBToGeoJson();
            String geoJson = vax.GetGeoJson(1);
            return View((object)geoJson);
        }
    }
}