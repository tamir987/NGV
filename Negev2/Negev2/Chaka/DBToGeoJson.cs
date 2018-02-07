using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Negev2.DataContext.Repositories;
using Negev2.Models;

namespace Negev2.Chaka
{
    public class DBToGeoJson
    {
        public string GetGeoJson(int id)
        {
            LayerRepository dbLayer = new LayerRepository();
            Layer layer = dbLayer.GetById(id);
            var model = new FeatureCollection();
            foreach (var item in layer.SitesByYear)
            {
                Site curSite = item.CurrentSite;
                var coordinates = new List<IPosition>();
                foreach (var x in curSite.Shape)
                {
                    coordinates.Add(new Position(x.Longtitude, x.Llatitude));
                }

                var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
              
                var props = new Dictionary<string, object>
                {
                    { "Ezor", curSite.Region },
                    { "SiteName", curSite.Name },
                    { "CropName", item.CurrentCrop.Name }
                };
                var feature = new GeoJSON.Net.Feature.Feature(polygon, props);
                model.Features.Add(feature);
            }
            return JsonConvert.SerializeObject(model);
        }
    }
}