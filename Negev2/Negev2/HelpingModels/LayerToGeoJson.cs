using Negev2.DataContext.Repositories;
using Negev2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Negev2.HelpingModels
{
    public partial class LayerToGeoJson
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "FeatureCollection";

        [JsonProperty("features")]
        public List<FeatureOfLayer> Features { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        public static string ToJson(LayerToGeoJson layer) => JsonConvert.SerializeObject(layer);
        
    }

    public partial class FeatureOfLayer
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "Feature";

        [JsonProperty("properties")]
        public PropertiesOfLayer Properties { get; set; }

        [JsonProperty("geometry")]
        public GeometryOfLayer Geometry { get; set; }
    }

    public partial class PropertiesOfLayer
    {
        //[JsonProperty("Dunam")]
        //public int Dunam { get; set; }

        [JsonProperty("Ezor")]
        public string Ezor { get; set; }

        [JsonProperty("Gidul")]
        public string CropName { get; set; }

        [JsonProperty("SiteName")]
        public string SiteName { get; set; }
    }

    public partial class GeometryOfLayer
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "Polygon";

        [JsonProperty("coordinates")]
        public List<CoordinateOfLayer> Coordinates { get; set; }
        //public List<List<List<CoordinateOfLayer>>> Coordinates { get; set; }
    }

    public partial struct CoordinateOfLayer
    {

        //public double Longtitude;

        //public double Llatitude;
        // public double? Double;
        
        public List<double> DoubleArray;
    }

    public partial class LayeredGeoJson
    {
        public string GetGeoJsonByLayer(int id)
        {
            LayerRepository dbLayer = new LayerRepository();
            Layer layer = dbLayer.GetById(id);
            LayerToGeoJson myGeo = new LayerToGeoJson {
               // Name = layer.Name,
                Features = new List<FeatureOfLayer>()
            };
            foreach (var item in layer.SitesByYear)
            {
                FeatureOfLayer currentFeature = new FeatureOfLayer();
                Site curSite = item.CurrentSite;
                currentFeature.Properties = new PropertiesOfLayer
                {
                    Ezor = curSite.Region,
                    SiteName = curSite.Name,
                    CropName = item.CurrentCrop.Name
                };
                List<CoordinateOfLayer> curCoordinates = new List<CoordinateOfLayer>();
                foreach (var x in curSite.Shape)
                {
                    CoordinateOfLayer temp = new CoordinateOfLayer
                    {
                        DoubleArray = new List<double>()
                    };
                    temp.DoubleArray.Add(x.Longtitude);
                    temp.DoubleArray.Add(x.Llatitude);
                    curCoordinates.Add(temp);
                }

                currentFeature.Geometry = new GeometryOfLayer
                {
                    Coordinates = curCoordinates
                };

                myGeo.Features.Add(currentFeature);

            }
            return LayerToGeoJson.ToJson(myGeo);
        }

    }
}