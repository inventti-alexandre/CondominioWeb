using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace SmartAdminMvc.Controllers
{
    public class BuildingsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Buildings
        public ActionResult Index()
        {
            try
            { 
            if (DataUtil.Validation())
                return View(db.Building.ToList());
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Buildings";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }
                
        // GET: Buildings/Create
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
                objError.page = "Buildings";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Buildings/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "buildingID,name,address,addressReference,country,state,city,district,apartmentQuantity,active,createDate,createUser,updateDate,updateUser")] Building building)
        {
            try
            {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        db.Building.Add(building);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(building);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Buildings";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            try { 
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Building building = db.Building.Find(id);
                if (building == null)
                {
                    return HttpNotFound();
                }
                return PartialView(building);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Buildings";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Buildings/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "buildingID,name,address,addressReference,country,state,city,district,apartmentQuantity,active,createDate,createUser,updateDate,updateUser")] Building building)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(building).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Buildings";
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
