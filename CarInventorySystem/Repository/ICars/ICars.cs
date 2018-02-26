using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarInventorySystem.DAL;

namespace CarInventorySystem.Repository.ICars
{
    public interface ICarsRepository
    {
        IQueryable<Car> FilterCars(string user);
        Car GetCar(int? id);
        bool CreateCar(Car cars,string user);
        bool UpdateCar(Car car);
        bool DeleteConfirm(int? id);


    }
}
