using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.Model;
using BuildingProject.DataAccess;
using System;

namespace BuildingProject.Controllers
{
    public class UsersController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: /User/
        public ActionResult Index()
        {
            if (DataUtil.Validation())
                return View(db.User.ToList().OrderBy(x => x.name));
            else
                return RedirectToAction("Login", "Home");
        }        

        // GET: /User/Create
        public ActionResult Create()
        {
            if (DataUtil.Validation())
                return PartialView();
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,name,lastname,username,password,active,email,dni,phonenumber,createDate,createUser,updateDate,updateUser")] User user)
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

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,name,lastname,username,password,active,email,dni,phonenumber,createDate,createUser,updateDate,updateUser")] User user)
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
              

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
