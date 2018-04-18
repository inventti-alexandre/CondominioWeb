using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.Model;
using BuildingProject.DataAccess;

namespace BuildingProject.Controllers
{
    public class HomeController : Controller
    {
        private BuildingContext db = new BuildingContext();
        
        public ActionResult Index()
        {
            if (DataUtil.Validation())
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        public ActionResult Login()
        {
            Session["USR_SESSION"] = "";
            Session["USR_OPCION"] = "[]";
            User objUser = new User();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Login(User objUser)
        {
            var user = db.User.FirstOrDefault(u => u.username == objUser.username && u.password == objUser.password && u.active == true);

            if (user != null)
            {
                user.messageWelcome = "Bienvenido(a) " + user.name + " " + user.lastname;
                Session["USR_SESSION"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                objUser.result = 1;
            }
            return View(objUser);
        }

        public ActionResult Recover()
        {
            User objUser = new User();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Recover(User objUser)
        {
            User oUser = new User();

            oUser = db.User.FirstOrDefault(u => u.username.ToUpper() == objUser.username.ToUpper());

            if (oUser != null)
            {
                DataUtil.SendMail("Estimado usuario(a), su contraseña es: " + oUser.password, "Recuperación de contraseña", oUser.email, "");
                objUser.result = 1;
            }
            else
            {
                objUser.result = 2;
            }
            return View(objUser);
        }

        public ActionResult UpdatePassword()
        {
            User objUser = new User();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult UpdatePassword(User objUser)
        {
            if (objUser.username == "" || objUser.password == "" || objUser.newpassword == "" || objUser.username == null || objUser.password == null || objUser.newpassword == null)
            {
                objUser.result = 3;
            }
            else
            {
                User oUser = new User();

                oUser = db.User.FirstOrDefault(u => u.username.ToUpper() == objUser.username.ToUpper() && u.password == objUser.password);

                if (objUser != null)
                {
                    oUser.password = objUser.newpassword;
                    db.Entry(oUser).State = EntityState.Modified;
                    db.SaveChanges();
                    objUser.result = 1;
                }
                else
                {
                    objUser.result = 2;
                }
            }
            return View(objUser);

        }

    }
}