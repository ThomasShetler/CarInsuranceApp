using CarInsuranceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsuranceApp.View_Models;

namespace CarInsuranceApp.Controllers

{ 
    public class HomeController : Controller
    {
        public int last;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool? dui, int tickets, bool? fullCoverage)
        {
          
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                int monthlytotal = 50;
                int age;
                using (NewsletterEntities db = new NewsletterEntities())
                {
                    var signup = new SignUps_();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;
                    signup.DateOfBirth = dateOfBirth;
                    signup.Dui = dui;
                    signup.FullCoverage = fullCoverage;
                    signup.CarMake = carMake;
                    signup.CarYear = carYear;
                    signup.Tickets = tickets;
                    signup.CarModel = carModel;
                  

                    age = DateTime.Now.Year - dateOfBirth.Year;
                    if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                    {
                        age = age - 1;
                    }

                    if (age < 25 && age > 18)
                    {
                        monthlytotal = monthlytotal + 25;
                    }
                    else if (age > 18)
                    {
                        monthlytotal = monthlytotal + 100;
                    }
                    else if (age > 100)
                    {
                        monthlytotal = monthlytotal + 25;
                    }
                    if (carYear < 2000 || carYear > 2015)
                    {
                        monthlytotal = monthlytotal + 25;
                    }
                    if (carMake == "Porsche")
                    {
                        monthlytotal = monthlytotal + 25;
                    }
                    if (carMake == "Porsche" && carModel == "911 Carrera")
                    {
                        monthlytotal = monthlytotal + 25;
                    }
                    monthlytotal = monthlytotal + (25 * tickets);

                    if (dui == true)
                    {
                        monthlytotal = Convert.ToInt32(monthlytotal * 1.25);
                    }
                    signup.MonthlyTotal = monthlytotal;
                    db.SignUps_.Add(signup);
                    db.SaveChanges();
                    return View("TotalQuote", signup);
                }
            }
        }
    
            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    
    }
}