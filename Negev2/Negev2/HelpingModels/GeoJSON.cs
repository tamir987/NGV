using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.SqlServer.Types;

namespace Negev2.HelpingModels
{

        public partial class GeoJSON
        {
            [JsonProperty("features")]
            public List<Feature> Features { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public partial class Feature
        {
            [JsonProperty("geometry")]
            public SqlGeometry Geometry { get; set; }
            //public Geometry Geometry { get; set; }

            [JsonProperty("properties")]
            public Properties Properties { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public partial class Properties
        {
            [JsonProperty("Details_12")]
            public string SixthDetails { get; set; }

            [JsonProperty("Details_15")]
            public string ThirdDetails { get; set; }

            [JsonProperty("Details_16")]
            public string SecondDetails { get; set; }

            [JsonProperty("Details_17")]
            public string FirstDetails { get; set; }

            [JsonProperty("Details_I_")]
            public string FifthDetails { get; set; }

            [JsonProperty("Dunam")]
            public int Dunam { get; set; }

            [JsonProperty("Ezor")]
            public string Ezor { get; set; }

            [JsonProperty("Gidul_I_13")]
            public string FifthCrop { get; set; }

            [JsonProperty("Gidul_I_14")]
            public string FourthCrop { get; set; }

            [JsonProperty("Gidul_I_15")]
            public string ThirdCrop { get; set; }

            [JsonProperty("Gidul_I_16")]
            public string SecondCrop { get; set; }

            [JsonProperty("Gidul_I_17")]
            public string FirstCrop { get; set; }

            [JsonProperty("Gudul_I_12")]
            public string SixthCrop { get; set; }


            [JsonProperty("Hel_Name")]
            public string HelName { get; set; }

            [JsonProperty("Sug_I_12")]
            public string SugSixth { get; set; }

            [JsonProperty("Sug_I_13")]
            public string SugFifth { get; set; }

            [JsonProperty("Sug_I_14")]
            public string SugFourth { get; set; }

            [JsonProperty("Sug_I_15")]
            public string SugThird { get; set; }

            [JsonProperty("Sug_I_16")]
            public string SugSecond { get; set; }

            [JsonProperty("Sug_I_17")]
            public string SugFirst { get; set; }
        }

        public partial class Geometry
        {
            [JsonProperty("coordinates")]
            public List<List<List<Coordinate>>> Coordinates { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public partial struct Coordinate
        {
            public double? Double;
            public List<double> DoubleArray;
        }

        public partial class GeoJSON
        {
            public static GeoJSON FromJson(string json) => JsonConvert.DeserializeObject<GeoJSON>(json, Converter.Settings);
        }

    public partial struct Coordinate
    {
        public Coordinate(JsonReader reader, JsonSerializer serializer)
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

    public static class Serialize
    {
        public static string ToJson(this GeoJSON self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Coordinate);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(Coordinate))
                return new Coordinate(reader, serializer);
            throw new Exception("Unknown type");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == typeof(Coordinate))
            {
                ((Coordinate)value).WriteJson(writer, serializer);
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
}