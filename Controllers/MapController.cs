using CsvHelper;
using MapAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MapAssignment.Controllers
{
    public class MapController : Controller
    {
        HttpClient objClient = new HttpClient();
        //
        // GET: /Map/

        public ActionResult MapIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Upload()
        {
            List<Address> lstAdd = new List<Address>();
            Address add = new Address();
            try
            {
                HttpPostedFileBase upload = Request.Files["file"];
                if (upload != null && upload.ContentLength > 0)
                {
                    List<string> lst = new List<string>();
                    Stream stream = upload.InputStream;

                    using (StreamReader streamReader = new StreamReader(stream))
                    {

                        var headers = streamReader.ReadLine().Split(',');

                        foreach (var item in headers)
                        {
                            add = new Address();
                            add.headers = item;
                            lstAdd.Add(add);
                        }

                    }

                    Parallel.ForEach(lstAdd, currentObj =>
                    {
                        try
                        {
                            var obj = GetAddressByGeocodingAPI(currentObj.headers.ToString());
                            foreach (var itm in obj.results[0].address_components)
                            {
                                if (itm.types.Contains("street_number"))
                                    currentObj.Unit = itm.short_name;

                                if (itm.types.Contains("route"))
                                    currentObj.Street = itm.short_name;

                                if (itm.types.Contains("neighborhood"))
                                    currentObj.Town = itm.short_name;

                                if (itm.types.Contains("administrative_area_level_1"))
                                    currentObj.State = itm.short_name;

                                if (itm.types.Contains("country"))
                                    currentObj.Country = itm.short_name;

                                if (itm.types.Contains("postal_code"))
                                    currentObj.ZipCode = itm.short_name;

                            }

                            currentObj.lat = obj.results[0].geometry.location.lat;
                            currentObj.lng = obj.results[0].geometry.location.lng;
                        }
                        catch (AggregateException ae)
                        {
                            ae.Handle((x) =>
                            {
                                if (x is ArgumentNullException)
                                {
                                    return true;
                                }
                                else if (x is UnauthorizedAccessException)
                                {
                                    return true;
                                }
                                else
                                {
                                    return true;
                                }
                            });
                        }

                    });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(lstAdd);
        }

        [HttpPost]
        public void ExportCSV(List<Address> lstAddress)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", "Unit", "Street", "Town/City ", "State", "ZipCode", "Country", Environment.NewLine);
                foreach (var item in lstAddress)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", item.Unit, item.Street, item.Town, item.State, item.ZipCode, item.Country, Environment.NewLine);
                }

                TempData["fname"] = sb.ToString();
            }
            catch (Exception ex)
            {

            }

        }

        [HttpGet]
        public ActionResult DownloadExcelReport()
        {
            string csv = TempData["fname"].ToString();
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Address.csv");
        }

        private Result GetAddressByGeocodingAPI(string address)
        {
            Result result = new Result();
            try
            {
                string url = "https://maps.googleapis.com/maps/api/geocode/json?&address=" + address + "&key=" + ConfigurationManager.AppSettings["GoogleAPIKey"];
                var response = objClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<Result>().Result;
                }

            }
            catch
            {
                throw;
            }
            return result;
        }

    }
}
