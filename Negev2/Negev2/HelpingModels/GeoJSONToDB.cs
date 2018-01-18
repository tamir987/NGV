using Negev2.DataContext.Repositories;
using Negev2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Negev2.HelpingModels
{
    public class GeoJSONToDB
    {
        private readonly int CurrentYear = DateTime.Now.Year;
        private CropRepository dbCrop = new CropRepository();
        private SiteRepository dbSite = new SiteRepository();
        private LayerRepository dbLayer = new LayerRepository();

        public GeoJSONToDB() { }

        public void AddToDB(String GeoJsonFile)
        {
            var data = GeoJSON.FromJson(GeoJsonFile);
            List<Layer> listOfLayers = new List<Layer>();

            for(int i = 0; i < 6; i++)
            {
                listOfLayers.Add(MakeLayer(i));
            }

            foreach (var item in data.Features)
            {
                Site site = null;
                var siteList = dbSite.GetMany(x => (x.Name.Equals(item.Properties.HelName) &&
                                                    x.Region.Equals(item.Properties.Ezor)));
                if (siteList.Count() > 0)
                    site = siteList.ElementAt(0);
                else
                {
                    site = new Site()
                    {
                        Name = item.Properties.HelName,
                        Dunam = item.Properties.Dunam,
                        Region = item.Properties.Ezor,
                        Shape = ConvertToCollection(item.Geometry.Coordinates, item.Geometry.Type),
                        SitesByYear = new Collection<SiteByYear>()
                    };
                    dbSite.Add(site);
                    dbSite.Save();
                }
                
                int i = 0;
                foreach(var layer in listOfLayers)
                {
                    i++;
                    String theCrop = GetCropName(i, item);

                    Crops crop = null;
                    var cropList = dbCrop.GetMany(x => x.Name.Equals(theCrop));
                    if (cropList.Count() > 0)
                        crop = cropList.ElementAt(0);
                    else
                    {
                        crop = new Crops()
                        {
                            Name = theCrop,
                            SitesByYear = new Collection<SiteByYear>()
                        };
                        dbCrop.Add(crop);
                        dbCrop.Save();
                    }

                    SiteByYear siteByYear = new SiteByYear()
                    {
                        CurrentLayer = layer,
                        CurrentLayerId = layer.Id,
                        CurrentSite = site,
                        CurrentSiteId = site.Id,
                        CurrentCrop = crop,
                        CurrentCropId = crop.Id
                    };
                    layer.SitesByYear.Add(siteByYear);
                }
            }
            SaveLayersToDB(listOfLayers);
        }

        private Layer MakeLayer(int i)
        {
            return new Layer()
            {
                Name = Convert.ToString(CurrentYear - i),
                Year = CurrentYear - i,
                SitesByYear = new Collection<SiteByYear>()
            };
        }

        private void SaveLayersToDB(List<Layer> listOfLayers)
        {
            foreach(Layer item in listOfLayers)
            {
                dbLayer.Add(item);
                dbLayer.Save();
            }
        }

        private ICollection<Coordinatez> ConvertToCollection(List<List<List<Coordinate>>> Coordinates, String type)
        {
            ICollection<Coordinatez> cur = new Collection<Coordinatez>();
            if (type.Equals("Polygon"))
            {
                var x = Coordinates.ElementAt(0);

                foreach (List<Coordinate> item in x)
                {
                    cur.Add(new Coordinatez()
                    {
                        Longtitude = (Double)item.ElementAt(0).Double,
                        Llatitude = (Double)item.ElementAt(1).Double,
                    });
                }
            }
            else if (type.Equals("MultiPolygon"))
            {
                foreach (List<List<Coordinate>> z in Coordinates)
                {
                    foreach (List<Coordinate> item in z)
                        cur.Add(new Coordinatez()
                        {
                            Longtitude = item.ElementAt(0).DoubleArray[0],
                            Llatitude = item.ElementAt(0).DoubleArray[1],
                        });
                }

            }
            return cur;
        }

        private String GetCropName(int i, Feature item)
        {
            StringBuilder builder = new StringBuilder();
            switch (i)
            {
                case 1:
                    builder.Append(item.Properties.FirstCrop).Append(" ").Append(item.Properties.SugFirst);
                    break;
                case 2:
                    builder.Append(item.Properties.SecondCrop).Append(" ").Append(item.Properties.SugSecond);
                    break;
                case 3:
                    builder.Append(item.Properties.ThirdCrop).Append(" ").Append(item.Properties.SugThird);
                    break;
                case 4:
                    builder.Append(item.Properties.FourthCrop).Append(" ").Append(item.Properties.SugFourth);
                    break;
                case 5:
                    builder.Append(item.Properties.FifthCrop).Append(" ").Append(item.Properties.SugFifth);
                    break;
                case 6:
                    builder.Append(item.Properties.SixthCrop).Append(" ").Append(item.Properties.SugSixth);
                    break;
            }
            if (builder.Equals(" "))
                builder.Replace(" ", "אין מידע");
            return builder.ToString();
        }

        public String GetMyJSON()
        {
            //System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\TAMIR\\Desktop\\Projects\\NGV Project\\Hel_Moshve_17.geojson");
            System.IO.StreamReader file = new System.IO.StreamReader(HostingEnvironment.MapPath(@"~/App_Data/geoJsonData.geojson"));
            string content = file.ReadToEnd();
            file.Close();
            return content;
        }
    }
}