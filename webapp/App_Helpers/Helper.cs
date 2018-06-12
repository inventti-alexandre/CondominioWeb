using System;

namespace SmartAdminMvc.App_Helpers
{
    public static class Helper
    {
        public static string DateFormat(DateTime datetime)
        {
            string result = "";
            result = datetime.ToShortDateString();
            return result;
        }
    }
}