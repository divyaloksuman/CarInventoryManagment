using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInventorySystem.DAL;
using PagedList;
using CarInventorySystem.Repository.Cars;

namespace CarInventorySystem.Controllers
{
    public class CarsInventoryController : Controller
    {
        #region Genral
        CarsRepository carRepository;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));

        public CarsInventoryController()
        {
            carRepository = new CarsRepository();
        }
        #endregion

        #region Create Edit Delete 
        // GET: Cars
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var cars = carRepository.FilterCars(HttpContext.User.Identity.Name);
            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Brand.Contains(searchString)
                                       || c.Model.Contains(searchString));
            }
            switch (sortOrder)
            {

                default:
                    cars = cars.OrderBy(s => s.Model);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(cars.ToPagedList(pageNumber, pageSize));
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            //Car car = new Car();
            return View();
            //return PartialView("_Create", car);
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Year,Price,New")] Car car)
        {
            if (ModelState.IsValid)
            {
                carRepository.CreateCar(car, HttpContext.User.Identity.Name);
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Year,Price,New")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.UName = HttpContext.User.Identity.Name;
                carRepository.UpdateCar(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carRepository.DeleteConfirm(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}