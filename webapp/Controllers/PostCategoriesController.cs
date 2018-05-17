using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace SmartAdminMvc.Controllers
{
    public class PostCategoriesController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: PostCategories
        public ActionResult Index()
        {
            try
            {
                if (DataUtil.Validation())
                    return View(db.PostCategory.ToList());
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "PostCategories";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }
                
        // GET: PostCategories/Create
        public ActionResult Create()
        {
            try {
                if (DataUtil.Validation())
                    return PartialView();
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "PostCategories";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: PostCategories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postCategoryID,name,active,createDate,createUser,updateDate,updateUser")] PostCategory postCategory)
        {
            try {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        db.PostCategory.Add(postCategory);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(postCategory);
                }
                else
                    return RedirectToAction("Login", "Home");                
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "PostCategories";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: PostCategories/Edit/5
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
                    PostCategory postCategory = db.PostCategory.Find(id);
                    if (postCategory == null)
                    {
                        return HttpNotFound();
                    }
                    return PartialView(postCategory);
                }
                else
                    return RedirectToAction("Login", "Home");
            }            
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "PostCategories";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: PostCategories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postCategoryID,name,active,createDate,createUser,updateDate,updateUser")] PostCategory postCategory)
        {
            try {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(postCategory).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(postCategory);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "PostCategories";
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
