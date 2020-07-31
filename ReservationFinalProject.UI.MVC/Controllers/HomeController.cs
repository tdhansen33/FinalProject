using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using ReservationFinalProject.UI.MVC.Models;

namespace ReservationFinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
          
            //Create the message
            string message = $"<strong>{cvm.Name}</strong><br/><strong>{cvm.Subject}</strong><br/><strong><em>{cvm.Email}</em></strong><br/><br/>{cvm.Message}";

            //Create the MailMessage object
            MailMessage msg = new MailMessage("no-reply@tylerdhansen.com", "hansen.tyler3@outlook.com", $"{System.DateTime.Now.Date} - {cvm.Subject}", message);

            //Customize MailMessage
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            //Allows you to reply directly to the person who filled out the form
            msg.ReplyToList.Add(cvm.Email);

            //Create SmtpClient
            SmtpClient client = new SmtpClient("mail.tylerdhansen.com");

            //Verify Credentials for client
            client.Credentials = new NetworkCredential("no-reply@tylerdhansen.com", "P@ssw0rd");

            //Attempt to send email
            try
            {
                client.Send(msg);
            }
            catch (System.Exception)
            {
                ViewBag.Error = "I'm sorry, there was an error handling your request. Please try again.";
            }
            return Json(cvm);
        }
    }
}
