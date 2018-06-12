using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;
using BuildingProject.ModelView;

namespace BuildingProject.Controllers
{
    public class RoleOptionsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: RoleOptions
        public ActionResult Index(int? id)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                if (id == null)
                    id = 0;

                RoleOptionView objRoleOptionView = new RoleOptionView();
                objRoleOptionView.roleOptionList = db.RoleOption.Include(u => u.option).Include(u => u.role).Where(u => u.role.roleID == id).ToList();
                objRoleOptionView.options = db.Database.SqlQuery<Option>("select o.optionid, o.name, CAST(ISNULL((select 1 from roleOptions ro where ro.roleID = @roleID and ro.optionID = o.optionID),0) AS BIT) selected, o.active, o.createdate, o.createuser, o.updatedate, o.updateuser from options o where o.active = 1", new SqlParameter("@roleID", id)).ToList();
                objRoleOptionView.role = db.Role.Find(id);
                return View(objRoleOptionView);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "RoleOptions";
                objError.option = "Index-1";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(RoleOptionView objRoleOptionView)
        {
            try
            { 
            if (DataUtil.Validation())
            {
                RoleOptionView objRoleOptionView2 = new RoleOptionView();
                try
                {
                    if (Request.Form["ActionForm"] == "Registrar")
                    {
                        var RemoveAll = db.RoleOption.Where(x => x.roleID == objRoleOptionView.role.roleID);
                        db.RoleOption.RemoveRange(RemoveAll);
                        db.SaveChanges();

                        RoleOption objRoleOption = new RoleOption();
                        foreach (var item in objRoleOptionView.options)
                        {
                            if (item.selected)
                            {
                                objRoleOption.roleID = objRoleOptionView.role.roleID;
                                objRoleOption.optionID = item.optionID;
                                db.RoleOption.Add(objRoleOption);
                                db.SaveChanges();
                            }
                        }



                        objRoleOptionView2.roleOptionList = db.RoleOption.Include(u => u.option).Include(u => u.role).Where(u => u.role.roleID == objRoleOptionView.role.roleID).ToList();
                        objRoleOptionView2.options = db.Database.SqlQuery<Option>("select o.optionid, o.name, CAST(ISNULL((select 1 from roleOptions ro where ro.roleID = @roleID and ro.optionID = o.optionID),0) AS BIT) selected, o.active, o.createdate, o.createuser, o.updatedate, o.updateuser from options o where o.active = 1", new SqlParameter("@roleID", objRoleOptionView.role.roleID)).ToList();
                        objRoleOptionView2.role = db.Role.Find(objRoleOptionView.role.roleID);

                        ModelState.Clear();
                    }



                }
                catch (Exception ex)
                {

                }
                return View(objRoleOptionView2);
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "RoleOptions";
                objError.option = "Index-2";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }

        
        public ActionResult Delete(int id)
        {
            try { 
            if (DataUtil.Validation())
            {
                var roleOption = db.RoleOption.Find(id);
                db.RoleOption.Remove(roleOption);
                db.SaveChanges();
                return RedirectToAction("Index", "RoleOptions", new { @id = roleOption.roleID });
            }
            else
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                Error objError = new Error();
                objError.page = "RoleOptions";
                objError.option = "Delete";
                objError.date = DateTime.Now;
                objError.description = ex.Message;
                BaseDataAccess<Error> baseDataAccess = new BaseDataAccess<Error>();
                baseDataAccess.Insert(objError);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
