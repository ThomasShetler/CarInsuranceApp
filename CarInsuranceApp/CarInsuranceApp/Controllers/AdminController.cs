using CarInsuranceApp.Models;
using CarInsuranceApp.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            using (NewsletterEntities db = new NewsletterEntities())
            {
                //var signups = db.SignUps_.Where(x => x.Removed == null).ToList();

                var signups = (from c in db.SignUps_
                               where c.Removed == null
                               select c);
                var signupVms = new List<SignupVm>();


                foreach (var signup in signups)
                {

                    var signupVm = new SignupVm();

                    signupVm.Id = signup.id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVm.MonthlyTotal = signup.MonthlyTotal;
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
        }
    }
}