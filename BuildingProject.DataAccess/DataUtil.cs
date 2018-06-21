using System;
using BuildingProject.Model;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BuildingProject.DataAccess
{
    public sealed class DataUtil
    {
        BuildingContext db = new BuildingContext();

        public static User GetUser()
        {
            if (HttpContext.Current.Session["USR_SESSION"] != "")
                return (User)HttpContext.Current.Session["USR_SESSION"];
            else
                return null;
        }
        public static bool Validation()
        {
            if (DataUtil.GetUser() == null)
                return false;
            else
                return true;
        }
        public bool ValidateOption(int user_id, int option_id)
        {
            var validate = (from x in db.UserRole join y in db.RoleOption on x.roleID equals y.roleID where x.userID == user_id && y.optionID == option_id select 1).Any();
            return validate;
        }
        public Option GetOption(int id)
        {
            return db.Option.FirstOrDefault(u => u.optionID == id);
        }
        public static Boolean SendMail(string mensaje, string asunto, string mailDestino, string url)
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("vhzambran87@gmail.com", "Administrador", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = asunto;
                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = mensaje;
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(mailDestino);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                if (url != "")
                {
                    mail.Attachments.Add(new Attachment(url));
                }
                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("vhzambran87@gmail.com", "vhjjmeVH87");
                SmtpServer.EnableSsl = true;

                SmtpServer.Host = "smtp.gmail.com";

                //Logger.Write("Envío de email");
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                //_log.Error("Error Email: " + ex.Message);
                return false;
            }

        }
    }
}
