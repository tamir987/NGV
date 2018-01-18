using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Negev2.Controllers
{
    public class AnalayzerController : Controller
    {
        // GET: AnalayzerIndex
        [HttpGet]
        public ActionResult AnalayzerIndex()
        {
            return View();
        }

        // GET: AnalayzerIndex after uploading a file
        [HttpPost]
        public ActionResult AnalayzerIndex(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = ".הקובץ עלה בהצלחה";
                return View();
            }
            catch
            {
                ViewBag.Message = "נסה שוב!";
                return View();
            }
        }
    }
}  