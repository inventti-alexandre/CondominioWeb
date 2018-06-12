using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;
using BuildingProject.ModelView;

namespace BuildingProject.Controllers
{
    public class ApartmentUsersController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: ApartmentUsers
        public ActionResult Index(int id)
        {
            try { 
            if (DataUtil.Validation())
            {
                var apartmentUser = db.ApartmentUser.Include(a => a.apartment).Include(a => a.user);
                ViewBag.ApartmentId = id;
                return View(apartmentUser.ToList().Where(x=>x.apartmentID == id));
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "ApartmentUsers";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ApartmentUsers/Create
        public ActionResult Create(int id)
        {
            try { 
            if (DataUtil.Validation())
            {
                User objUser = new User();
                objUser.apartmentID = id;
                return View(objUser);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "ApartmentUsers";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ApartmentUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    UserRole objUserRole = new UserRole();
                    objUserRole.userID = user.userID;
                    objUserRole.roleID = 4;                
                    db.UserRole.Add(objUserRole);
                    db.SaveChanges();
                    ApartmentUser objApartmentUser = new ApartmentUser();
                    objApartmentUser.userID = user.userID;
                    objApartmentUser.apartmentID = user.apartmentID;
                    objApartmentUser.principal = user.principal;
                    db.ApartmentUser.Add(objApartmentUser);
                    db.SaveChanges();                
                    return RedirectToAction("Index", new { id = user.apartmentID });
                }
                return View(user);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "ApartmentUsers";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ApartmentUsers/Edit/5
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
                    ApartmentUser apartmentUser = db.ApartmentUser.Include(u => u.user).FirstOrDefault(u => u.apartmentUserID == id);
                    apartmentUser.user.apartmentID = apartmentUser.apartmentID;
                    apartmentUser.user.principal = apartmentUser.principal;
                    if (apartmentUser == null)
                    {
                        return HttpNotFound();
                    }
                    return View(apartmentUser.user);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "ApartmentUsers";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ApartmentUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( User user)
        {
            try
            {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        BaseDataAccess<User> objBaseDatosAccess = new BaseDataAccess<User>();
                        objBaseDatosAccess.Update(user);
                        ApartmentUser objApartmentUser = new ApartmentUser();
                        objApartmentUser.apartmentID = user.apartmentID;
                        objApartmentUser.userID = user.userID;
                        objApartmentUser.principal = user.principal;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = user.apartmentID });
                    }
                    return View(user);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "ApartmentUsers";
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
