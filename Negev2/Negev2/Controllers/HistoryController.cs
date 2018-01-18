using Negev2.DataContext.Repositories;
using Negev2.HelpingModels;
using Negev2.Models;
using Negev2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Negev2.Controllers
{
    public class HistoryController : Controller
    {

        private ILayerRepository db = new LayerRepository();


        public HistoryController()
        {

        }
        // GET: History
        public ActionResult HistoryIndex()
        {
            var listOfLayers = db.GetAll();
            var idToNameList = new Collection<IdToName>();
            foreach (var item in listOfLayers)
            {
                idToNameList.Add(new IdToName(item.Id, item.Name));
            }
            HistoryViewModel model = new HistoryViewModel
            {
                LayersIdToName = idToNameList   
            };
            return View(model);
        }

        public ActionResult GetLayersByIds(List<int> LayersId)
        {
            var layersToReturn = new Collection<Layer>();
            foreach(var item in LayersId)
            {
                layersToReturn.Add(db.GetById(item));
            }
            HistoryViewModel model = new HistoryViewModel
            {
                LayersCollection = layersToReturn
            };
            return View(model);
        }
    }
}