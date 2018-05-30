using Newtonsoft.Json;
using ReceiptScanner.ContractResolvers;
using ReceiptScanner.DAL;
using ReceiptScanner.Models;
using ReceiptScanner.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReceiptScanner.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Scan(string base64String)
        {
            var imageParts = base64String.Split(',').ToList<string>();
            byte[] imageBytes = Convert.FromBase64String(imageParts[1]);

            using (var client = new HttpClient())
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

                string uri = "https://vision.googleapis.com/v1/images:annotate?key=" + "AIzaSyAAghI5eA0GCMLARpjSv8tp7HxB5cU28xg";

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCaseContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                string jsonRequest = JsonConvert.SerializeObject(googleVision, Formatting.None, settings);

                var response = await client.PostAsync(uri, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                var content = response.Content.ReadAsStringAsync();

                return Json(data: content.Result);
            }
        }

        [HttpPost]
        public ActionResult Save(Receipt receipt)
        {
            if (Session["userID"] == null || Session["accountID"] == null)
            {
                return null;
            }
            var db = new DatabaseContext();
            db.Receipts.Add(
                new Receipt
                {
                    Name = receipt.Name,
                    Date = receipt.Date,
                    Content = receipt.Content,
                    Price = receipt.Price,
                    Category = receipt.Category,
                    Notes = receipt.Notes,
                    Base64_Image = receipt.Base64_Image,
                    Currency_Id = receipt.Currency_Id,
                    Account_Id = Convert.ToInt32(Session["accountID"]),
                    User_Id = Convert.ToInt32(Session["userID"])
                }
            );
            db.SaveChanges();
            return View(receipt);
        }
    }
}