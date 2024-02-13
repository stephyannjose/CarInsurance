using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Admin()
        {
            var quotes = db.Insurees.ToList(); // Retrieve all quotes issued
            return View(quotes);
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
    public class InsuranceQuoteCalculator
    {
        public static double CalculateQuote(int age, int carYear, string carMake, string carModel, int speedingTickets, bool hasDUI, bool isFullCoverage)
        {
            double baseQuote = 50;

            // Age criteria
            if (age <= 18)
                baseQuote += 100;
            else if (age >= 19 && age <= 25)
                baseQuote += 50;
            else
                baseQuote += 25;

            // Car year criteria
            if (carYear < 2000)
                baseQuote += 25;
            else if (carYear > 2015)
                baseQuote += 25;

            // Car make criteria
            if (carMake == "Porsche")
            {
                baseQuote += 25;

                // Car model criteria
                if (carModel == "911 Carrera")
                    baseQuote += 25;
            }

            // Speeding tickets criteria
            baseQuote += speedingTickets * 10;

            // DUI criteria
            if (hasDUI)
                baseQuote *= 1.25;

            // Full coverage criteria
            if (isFullCoverage)
                baseQuote *= 1.5;

            return baseQuote;
        }

        static void Main(string[] args)
        {
            // Sample usage
            int age = 20;
            int carYear = 2010;
            string carMake = "Porsche";
            string carModel = "911 Carrera";
            int speedingTickets = 2;
            bool hasDUI = false;
            bool isFullCoverage = true;

            double quote = CalculateQuote(age, carYear, carMake, carModel, speedingTickets, hasDUI, isFullCoverage);
            Console.WriteLine("Insurance Quote: $" + quote);
        }
    }

}
