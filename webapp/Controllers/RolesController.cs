using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace BuildingProject.Controllers
{
    public class RolesController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Roles
        public ActionResult Index()
        {
            try { 
            if (DataUtil.Validation())
                return View(db.Role.ToList().OrderBy(x => x.name));
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Roles";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Roles/Create
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
                objError.page = "Roles";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleID,name,active,createDate,createUser,updateDate,updateUser,selected")] Role role)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    role.createDate = DateTime.Now;
                    role.createUser = Helper.GetCurrentUser().userID;
                    db.Role.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Roles";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return PartialView(role);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Roles";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleID,name,active,createDate,createUser,updateDate,updateUser,selected")] Role role)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    role.updateDate = DateTime.Now;
                    role.updateUser = Helper.GetCurrentUser().userID;
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Roles";
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
    }
}
