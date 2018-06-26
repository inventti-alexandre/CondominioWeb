using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.Model;
using BuildingProject.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageResizer;

namespace BuildingProject.Controllers
{
    public class UsersController : Controller
    {
        private BuildingContext db = new BuildingContext();


        public ActionResult Profile()
        {
            try
            {
                if (DataUtil.Validation())
                {
                    User user = db.User.Find(Helper.GetCurrentUser().userID);
                    user.imageURL = Helper.GetCurrentUserLogo();

                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Profile-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "userID,categoryUserID,name,lastname,username,password,active,email,dni,phonenumber,createDate,createUser,updateDate,updateUser")] User user, HttpPostedFileBase postedFile)
        {
            try
            {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        var objUserEmail = db.User.FirstOrDefault(u => u.email == user.email && u.userID != user.userID);
                        if (objUserEmail != null)
                        {
                            user.messageErrorEmail = "El email ingresado ya existe!.";
                        }
                        else
                        {
                            user.updateDate = DateTime.Now;
                            user.updateUser = Helper.GetCurrentUser().userID;
                            user.name = user.name.ToUpper();
                            user.lastname = user.lastname.ToUpper();

                            user.imageURL = System.Configuration.ConfigurationManager.AppSettings["userImgURL"] + user.userID.ToString() + ".jpg";
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();

                            if (postedFile != null)
                            {
                                var versions = new Dictionary<string, string>();
                                string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["userImgURLFinal"]);
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                postedFile.SaveAs(path + Helper.GetCurrentUser().userID.ToString() + Path.GetExtension(postedFile.FileName));
                                versions.Add("", "maxwidth=120&maxheight=120&format=jpg");
                                foreach (var suffix in versions.Keys)
                                {
                                    postedFile.InputStream.Seek(0, SeekOrigin.Begin);
                                    ImageBuilder.Current.Build(
                                        new ImageJob(
                                            postedFile.InputStream,
                                            path + user.userID.ToString() + suffix,
                                            new Instructions(versions[suffix]),
                                            false,
                                            true));
                                }
                                ViewBag.Message = "File uploaded successfully.";
                            }
                        }
                        return RedirectToAction("Profile");
                    }
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Edit-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }


        // GET: /User/
        public ActionResult Index()
        {
            try
            { 
            if (DataUtil.Validation())
                return View(db.User.ToList().OrderBy(x => x.name));
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }        

        // GET: /User/Create
        public ActionResult Create()
        {
            try
            { 
            if (DataUtil.Validation())
                return PartialView();
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,name,lastname,username,password,active,email,dni,phonenumber,createDate,createUser,updateDate,updateUser")] User user)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    var objUser = db.User.FirstOrDefault(u => u.username == user.username);
                    var objUserEmail = db.User.FirstOrDefault(u => u.email == user.email);
                    if (objUser == null && objUserEmail == null)
                    {
                        user.createDate = DateTime.Now;
                        user.createUser = Helper.GetCurrentUser().userID;
                        user.name = user.name.ToUpper();
                        user.lastname = user.lastname.ToUpper();
                        db.User.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (objUser != null)
                            user.messageErrorUser = "El usuario ingresado ya existe!.";

                        if (objUserEmail != null)
                            user.messageErrorEmail = "El email ingresado ya existe!.";

                        return View(user);
                    }
                }
                return View(user);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return PartialView(user);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,name,lastname,username,password,active,email,dni,phonenumber,createDate,createUser,updateDate,updateUser")] User user)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    var objUserEmail = db.User.FirstOrDefault(u => u.email == user.email && u.userID != user.userID);
                    if (objUserEmail != null)
                    {
                        user.messageErrorEmail = "El email ingresado ya existe!.";
                    }
                    else
                    {
                        user.updateDate = DateTime.Now;
                        user.updateUser = Helper.GetCurrentUser().userID;
                        user.name = user.name.ToUpper();
                        user.lastname = user.lastname.ToUpper();
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Users";
                objError.option = "Edit-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }
              

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult getHistory(string chatCode)
        {
            BuildingContext db = new BuildingContext();
            var chatList = (from x in db.Chat where x.chatCode == chatCode orderby x.createDate select x.message).ToList();
            return Json(chatList, JsonRequestBehavior.AllowGet);
        }
    }
}
