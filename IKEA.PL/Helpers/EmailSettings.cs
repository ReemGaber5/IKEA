using IKEA.DAL.Models.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net;
using System.Net.Mail;

namespace IKEA.PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            //handel host and port
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("reemgaber213@gmail.com", "bphciryyrnxggmcp");//use app password(for one app)
            Client.Send("reemgaber213@gmail.com",email.To,email.Subject,email.Body);
            


        }
    }
}
