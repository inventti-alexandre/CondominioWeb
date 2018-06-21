using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.Model;
using BuildingProject.DataAccess;
using System;

namespace BuildingProject.Controllers
{
    public class HomeController : Controller
    {
        private BuildingContext db = new BuildingContext();
        
        public ActionResult Index()
        {
            try { 
            if (DataUtil.Validation())
                return View();
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Login()
        {
            try
            { 
            Session["USR_SESSION"] = "";
            Session["USR_OPCION"] = "[]";
            User objUser = new User();
            return View(objUser);
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "Login-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Login(User objUser)
        {
            try
            {
                //var user = db.User.FirstOrDefault(u => u.username == objUser.username && u.password == objUser.password && u.active == true);
                var user = (from x in db.User where x.username == objUser.username && x.password == objUser.password && x.active == true select x).FirstOrDefault();

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
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "Login-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Recover()
        {
            try { 
            User objUser = new User();
            return View(objUser);
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "Recover-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Recover(User objUser)
        {
            try { 
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
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "Recover-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult UpdatePassword()
        {
            try
            {
                User objUser = new User();
                return View(objUser);
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "UpdatePassword-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdatePassword(User objUser)
        {
            try
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
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Home";
                objError.option = "UpdatePassword-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }

        }

    }
}