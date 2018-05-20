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
    public class PostsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Posts
        public ActionResult Index()
        {
            try
            {
                if (DataUtil.Validation())
                {
                    var post = db.Post.Include(p => p.postCategory);
                    return View(post.ToList());
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Post";
                objError.option = "Index";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
            }

        // GET: Posts/Create
        public ActionResult Create()
        {
            try
            {
                if (DataUtil.Validation())
                {
                    ViewBag.postCategoryID = new SelectList(db.PostCategory, "postCategoryID", "name");
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Post";
                objError.option = "Create-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }
        // POST: Posts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postID,postCategoryID,title,content,active,createDate,createUser,updateDate,updateUser")] Post post)
        {
            try
            {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        db.Post.Add(post);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                        ViewBag.postCategoryID = new SelectList(db.PostCategory, "postCategoryID", "name", post.postCategoryID);
                        return View(post);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Post";
                objError.option = "Create-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Posts/Edit/5
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
                    Post post = db.Post.Find(id);
                    if (post == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.postCategoryID = new SelectList(db.PostCategory, "postCategoryID", "name", post.postCategoryID);
                    return PartialView(post);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Post";
                objError.option = "Edit-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Posts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postID,postCategoryID,title,content,active,createDate,createUser,updateDate,updateUser")] Post post)
        {
            try
            {
                if (DataUtil.Validation())
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(post).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.postCategoryID = new SelectList(db.PostCategory, "postCategoryID", "name", post.postCategoryID);
                    return View(post);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "Post";
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
