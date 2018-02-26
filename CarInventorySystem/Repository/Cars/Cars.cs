using CarInventorySystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInventorySystem.Repository.Cars
{
    public class CarsRepository : ICars.ICarsRepository
    {
        private CarDBEntities db = new CarDBEntities();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CarsRepository)); 
        public IQueryable<Car> FilterCars(string user)
        {
            dynamic result;
            try
            {
                var rcars = from s in db.Cars
                            select s;
                rcars = rcars.Where(u => u.UName == user);
                result= rcars;
            }
            catch(Exception ex){
                logger.Error(ex.ToString());
                result = new Car();
            }
            return result;
        }

        public DAL.Car GetCar(int? id)
        {
            return db.Cars.Find(id);
        }

        public bool CreateCar(DAL.Car cars, string user)
        {
            cars.UName = user;
            db.Cars.Add(cars);
            db.SaveChanges();

            return true;
        }

        public bool UpdateCar(DAL.Car car)
        {
            db.Entry(car).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return true;
        }
              

        public bool DeleteConfirm(int? id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return true;
        }
       
       
    }
}

