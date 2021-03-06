﻿using System;
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
            int i;
            foreach (var item in layer.SitesByYear)
            {
                i = 0;
                Site curSite = item.CurrentSite;
                //if (curSite.Shape.Count > 3)
                //{
                    var coordinates = new List<IPosition>();
                var multi = new List<Polygon>();
                    foreach (var x in curSite.Shape)
                    {
                        if (x.Llatitude == 0 && x.Longtitude == 0)
                    {
                      //  multi.Add(new Polygon(new List<LineString> { new LineString(coordinates) }));
                      //  coordinates.Clear();
                      //  i = 1;
                    }
                         
                        else
                        coordinates.Add(new Position(x.Longtitude, x.Llatitude));
                }

                var props = new Dictionary<string, object>
                {
                    { "Ezor", curSite.Region },
                    { "SiteName", curSite.Name },
                    { "CropName", item.CurrentCrop.Name }
                };
                GeoJSON.Net.Feature.Feature feature = null;
                if (i == 1)
                   feature = new GeoJSON.Net.Feature.Feature(new MultiPolygon(multi), props);
                else
                    feature = new GeoJSON.Net.Feature.Feature(new Polygon(new List<LineString> { new LineString(coordinates) }), props);
                    model.Features.Add(feature);

              //  }
                //else
                //{
                //    var coordinates = new List<Point>();
                //    foreach (var x in curSite.Shape)
                //    {
                //        coordinates.Add(new Point (new Position(x.Longtitude, x.Llatitude) ));
                //    }
                //    var polygon = new MultiPoint(coordinates);
                //    var props = new Dictionary<string, object>
                //{
                //    { "Ezor", curSite.Region },
                //    { "SiteName", curSite.Name },
                //    { "CropName", item.CurrentCrop.Name }
                //};
                //    var feature = new GeoJSON.Net.Feature.Feature(polygon, props);
                //    model.Features.Add(feature);
                //}
                   

               
            }
    //        string x = JsonConvert.SerializeObject(model);
  //          FeatureCollection my = JsonConvert.DeserializeObject<FeatureCollection>(x);
  

            return JsonConvert.SerializeObject(model);
        }
    }
}