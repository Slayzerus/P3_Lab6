using CarBuilder;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace CarBuilder
{
    public class Program
    {
        private static BackgroundWorker inputListener = new BackgroundWorker();
        private static string inputTimeString = string.Empty;
        private static Car? car;

        public static void Main()
        {
            ServiceProvider DICollection = new ServiceCollection()
                       .AddScoped<ICar, Car>()
                       .AddScoped<IEngine, Engine>()
                       .AddScoped<IFuelTank, FuelTank>()
                       .AddScoped<IFuelTankDisplay, FuelTankDisplay>()
                       .AddScoped<IDrivingInformationDisplay, DrivingInformationDisplay>()
                       .AddScoped<IDrivingProcessor, DrivingProcessor>()
                       .BuildServiceProvider();

            car = (Car?)DICollection.GetService<ICar>();

            if (car != null)
            {
                inputListener.DoWork += InputListener_ReadInput;
                inputListener.RunWorkerCompleted += InputListener_ReadCompleted;
                inputListener.RunWorkerAsync();
                car.EngineStart();
                RefreshCar();
            }
        }

        private static void RefreshCar()
        {
            while (!car._fuelTankDisplay.IsOnReserve)
            {
                if (car._drivingProcessor.ActualSpeed == 0)
                {
                    car.RunningIdle();
                }
                else
                {
                    if (inputTimeString != DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                    {
                        car.FreeWheel();
                    }

                    car._engine.Consume(car.FuelConsumption);
                }

                DisplayCarInfo();
                Thread.Sleep(1000);
                Console.Clear();
            }
            car.EngineStop();
        }

        private static void DisplayCarInfo()
        {
            Console.WriteLine($"Speed: {car._drivingInformationDisplay.ActualSpeed} km/h");
            Console.WriteLine($"Fuel: {car._fuelTankDisplay.FillLevel}");
            Console.WriteLine($"Fuel Consumption: {car.FuelConsumption}");
            Console.WriteLine("w - accelerate; s - break");
        }

        private static void InputListener_ReadInput(object sender, DoWorkEventArgs e)
        {
            if (Console.KeyAvailable == false)
            {
                Thread.Sleep(100);
            }
            else
            {
                char inputKey = Console.ReadKey(true).KeyChar;
                if (inputKey == 'w' && inputTimeString != DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                {
                    inputTimeString = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    car.Accelerate(car.Acceleration);
                }
                else if (inputKey == 's' && inputTimeString != DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                {
                    inputTimeString = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    car.BrakeBy(10);
                }
            }
        }

        private static void InputListener_ReadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!inputListener.IsBusy)
            {
                inputListener.RunWorkerAsync(); // restart the worker
            }
        }
    }
}

