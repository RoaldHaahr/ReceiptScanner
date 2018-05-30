using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReceiptScanner.ContractResolvers;
using ReceiptScanner.Models;

namespace ReceiptScanner.Controllers
{
    public class CamGoogleController : Controller
    {
        // GET: CamGoogle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Capture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Capture(string base64String)
        {
            var imageParts = base64String.Split(',').ToList<string>();
            byte[] imageBytes = Convert.FromBase64String(imageParts[1]);

            using (var client = new WebClient())
            {
                GoogleVision googleVision = new GoogleVision()
                {
                    Requests = new List<Request>()
                    {
                        new Request()
                        {
                            Image = new Image()
                            {
                                Content = imageParts[1]
                            },

                            Features = new List<Feature>()
                            {
                                new Feature()
                                {
                                    Type = "TEXT_DETECTION"

                                }
                            },
                            ImageContext = new ImageContext()
                            {
                                LanguageHints = new List<string>()
                                {
                                    "da"
                                }
                            }
                        }
                    }
                };

                var uri = "https://vision.googleapis.com/v1/images:annotate?key=" + "AIzaSyAAghI5eA0GCMLARpjSv8tp7HxB5cU28xg";

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCaseContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                var jsonRequest = JsonConvert.SerializeObject(googleVision, Formatting.None, settings);

                var response = client.UploadString(uri, jsonRequest);

                return Json(data: response);
            }
        }
    }
}