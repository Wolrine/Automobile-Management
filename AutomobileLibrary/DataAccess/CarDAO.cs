namespace AutomobileLibrary.DataAccess;

internal class CarDAO
{
    private static CarDAO _instance = null;
    private static readonly object _instanceLock = new();
    public static CarDAO Instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new CarDAO();
                }
                return _instance;
            }
        }
    }

    public IEnumerable<Car> GetCarList()
    {
        var cars = new List<Car>();
        try
        {
            using var context = new MyStockContext();
            cars = context.Cars.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return cars;
    }

    public Car GetCarByID(int carID)
    {
        Car car = null;
        try
        {
            using var context = new MyStockContext();
            car = context.Cars.SingleOrDefault(c => c.CarId == carID);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return car;
    }

    public void AddNew(Car car)
    {
        try
        {
            Car _car = GetCarByID(car.CarId);
            if (_car == null)
            {
                using var context = new MyStockContext();
                context.Cars.Add(car);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("The car is already exist.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public void Update(Car car)
    {
        try
        {
            Car _car = GetCarByID(car.CarId);
            if (_car != null)
            {
                using var context = new MyStockContext();
                context.Cars.Update(car);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("The car does not already exist.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public void Remove(int carID)
    {
        try
        {
            Car car = GetCarByID(carID);
            if (car != null)
            {
                using var context = new MyStockContext();
                context.Cars.Remove(car);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("The car does not already exist.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
