using ReceiptScanner.DAL;
using ReceiptScanner.Models.EntityModels;
using System.Linq;
using System.Web.Mvc;

namespace ReceiptScanner.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var db = new DatabaseContext();
            var email = collection["email"];
            var password = collection["password"];
            var user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                ViewBag.errorMessage = "Wrong email and/or password.";
                return View();
            }

            Session["userID"] = user.Id;
            //return View("../Home/Index");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            if(collection["password_repeat"] != collection["password"])
            {
                ViewBag.errorMessage = "Passwords do not match";
                return View();
            }
            var db = new DatabaseContext();
            var user = new User
            {
                Email = collection["email"],
                Name = collection["first_name"] + " " + collection["last_name"],
                Password = collection["password"],
                Language_Code = "EN"
            };
            db.Users.Add(user);
            db.SaveChanges();
            return View("Login");
        }
    }
}