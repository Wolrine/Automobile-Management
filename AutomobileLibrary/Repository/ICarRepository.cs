using AutomobileLibrary.DataAccess;

namespace AutomobileLibrary.Repository;

public interface ICarRepository
{
    IEnumerable<Car> GetCars();
    Car GetCarById(int carId);
    void DeleteCar(int carId);
    void UpdateCar(Car car);
    void InsertCar(Car car);
}
