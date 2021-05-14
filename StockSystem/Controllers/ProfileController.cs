using StockSystem.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StockSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Profile
        public ActionResult Index()
        {
            var id = User.Identity.Name;

            var uid = db.Users.Where(c => c.UserName == id).SingleOrDefault();

            var employee = db.Employees.Where(c => c.UniqueId == uid.UniqueId).SingleOrDefault();
            return View(employee);
        }
        [HttpPost]
        public ActionResult Index(int hours)
        {
            var id = User.Identity.Name;

            var uid = db.Users.Where(c => c.UserName == id).SingleOrDefault();

            var employee = db.Employees.Where(c => c.UniqueId == uid.UniqueId).SingleOrDefault();
            ViewBag.wages = float.Parse(employee.WagePH) * hours;
            return View(employee);
        }


        public ActionResult Mail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Mail(string Subject,string Message)
        {
            string body = Message;
            string senderemail = User.Identity.GetUserName();

            MailMessage mail = new MailMessage();
            mail.Subject = Subject;
            mail.From = new MailAddress(senderemail);
            mail.To.Add("shaunmarron94@gmail.com");
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("shaunmarron94@gmail.com", "mg@32108294");
            smtp.Credentials = netCre;
            smtp.Send(mail);
            ViewBag.send = "Your mail has been forwarded to Management";
            return View();
        }
    }
}