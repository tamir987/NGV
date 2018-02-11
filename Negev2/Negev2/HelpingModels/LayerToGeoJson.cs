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

        //public static string ToJson(LayerToGeoJson layer) => JsonConvert.SerializeObject(layer);
        
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
          public List<List<List<double>>> Coordinates { get; set; }
        //public List<List<List<CoordinateOfLayer>>> Coordinates { get; set; }
    }

    public partial struct CoordinateOfLayer
    {

        public double? Double;
        public List<double> DoubleArray;
    }

    public partial struct CoordinateOfLayer
    {
        public CoordinateOfLayer(JsonReader reader, JsonSerializer serializer)
        {
            Double = null;
            DoubleArray = null;

            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    Double = serializer.Deserialize<double>(reader);
                    break;
                case JsonToken.StartArray:
                    DoubleArray = serializer.Deserialize<List<double>>(reader);
                    break;
                default: throw new Exception("Cannot convert Coordinate");
            }
        }

        public void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            if (Double != null)
            {
                serializer.Serialize(writer, Double);
                return;
            }
            if (DoubleArray != null)
            {
                serializer.Serialize(writer, DoubleArray);
                return;
            }
            throw new Exception("Union must not be null");
        }
    }


    public class ConverterO : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CoordinateOfLayer);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(CoordinateOfLayer))
                return new CoordinateOfLayer(reader, serializer);
            throw new Exception("Unknown type");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == typeof(CoordinateOfLayer))
            {
                ((CoordinateOfLayer)value).WriteJson(writer, serializer);
                return;
            }
            throw new Exception("Unknown type");
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new Converter() },
        };
    }

    public static class Serializeo
    {
        public static string ToJson(this LayerToGeoJson self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
                // List<List<List<CoordinateOfLayer>>> curCoordinates = new List<List<List<CoordinateOfLayer>>>();
                // List<List<CoordinateOfLayer>> Insu = new List<List<CoordinateOfLayer>>();
                List<List<List<double>>> curCoordinates = new List<List<List<double>>>();
                 List<List<double>> Insu = new List<List<double>>();
                foreach (var x in curSite.Shape)
                {
                    //List<CoordinateOfLayer> temp = new List<CoordinateOfLayer>();
                    //CoordinateOfLayer first = new CoordinateOfLayer { Double = x.Longtitude };
                    //CoordinateOfLayer second = new CoordinateOfLayer { Double = x.Llatitude };
                    List<double> temp = new List<double>();
                   // CoordinateOfLayer first = new CoordinateOfLayer { Double = x.Longtitude };
                    //CoordinateOfLayer second = new CoordinateOfLayer { Double = x.Llatitude };
                    temp.Add(x.Longtitude);
                    temp.Add(x.Llatitude);
                    //CoordinateOfLayer temp = new CoordinateOfLayer
                    //{
                    //    DoubleArray = new List<double>()
                    //};

                    Insu.Add(temp);
                }
                curCoordinates.Add(Insu);

                currentFeature.Geometry = new GeometryOfLayer
                {
                    Coordinates = curCoordinates
                };

                myGeo.Features.Add(currentFeature);

            }
            return Serializeo.ToJson(myGeo);
        }

    }
}