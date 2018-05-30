using System.Web.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                ViewBag.errorMessage = "Login to see this page";
                return null;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index(FormCollection form)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyAAghI5eA0GCMLARpjSv8tp7HxB5cU28xg", new StringContent(form.ToString(), Encoding.UTF8, "application/json"));
            
            return Json(response);
        }
    }
}