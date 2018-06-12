using BuildingProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildingProject
{
    public static class Helper
    {
        public static string DateFormat(DateTime? datetime)
        {
            string result = "";
            if (datetime != null)
            {
                result = Convert.ToDateTime(datetime).ToShortDateString();
            }
            return result;
        }
        public static User GetCurrentUser()
        {
            return (User)(HttpContext.Current.Session["USR_SESSION"]);
        }
        public static DateTime GetDateNow(DateTime date)
        {
            return date.Date;
        }
        public static DateTime GetDateNext(DateTime date)
        {
            return date.Date.AddDays(1);
        }
        public static string GetTime(DateTime date)
        {
            return date.ToShortTimeString();
        }
        public static List<SelectListItem> DropDown(IEnumerable data, string value, string text, int selected = -1)
        {
            List<SelectListItem> dropDown;
            if (selected == -1 && selected == 0)
                dropDown = new SelectList(data, value, text).ToList();
            else
                dropDown = new SelectList(data, value, text, selected).ToList();
            return dropDown;
        }
        public static List<SelectListItem> DropDownVoid(IEnumerable data, string value, string text, int selected = -1)
        {
            List<SelectListItem> dropDown;
            if (selected == -1 && selected == 0)
            {
                dropDown = new SelectList(data, value, text).ToList();
                dropDown.Insert(0, (new SelectListItem { Text = "[Seleccionar]", Value = "0" }));
            }
            else
            {
                dropDown = new SelectList(data, value, text, selected).ToList();
                dropDown.Insert(0, (new SelectListItem { Text = "[Seleccionar]", Value = "0" }));
            }
            return dropDown;
        }
    }
}