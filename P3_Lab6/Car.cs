using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBuilder
{
    public class Car : ICar
    {
        public readonly IEngine _engine;

        public readonly IFuelTank _fuelTank;

        public readonly IFuelTankDisplay _fuelTankDisplay;

        public readonly IDrivingInformationDisplay _drivingInformationDisplay;

        public readonly IDrivingProcessor _drivingProcessor;

        private int acceleration = 10;

        public int Id { get; set; }

        public bool EngineIsRunning => _engine.IsRunning;

        public double FuelConsumption
        {
            get
            {                
                if (_drivingProcessor.ActualSpeed < 1)
                {
                    return 0;
                }
                else if (_drivingProcessor.ActualSpeed <= 60)
                {
                    return 0.002;
                }
                else if (_drivingProcessor.ActualSpeed <= 100)
                {
                    return 0.0014;
                }
                else if(_drivingProcessor.ActualSpeed <= 140)
                {
                    return 0.002;
                }
                else if (_drivingProcessor.ActualSpeed <= 200)
                {
                    return 0.0025;
                }
                else
                {
                    return 0.003;
                }
            }
        }

        public int Acceleration 
        {
            get => acceleration;
            set
            {
                acceleration = value;
                acceleration = acceleration < 5 ? 5 : acceleration;
                acceleration = acceleration > 20 ? 20 : acceleration;
            } 
        }

        public Car(
            IEngine engine,
            IFuelTank fuelTank,
            IFuelTankDisplay fuelTankDisplay,
            IDrivingInformationDisplay drivingInformationDisplay,
            IDrivingProcessor drivingProcessor, 
            int acceleration = 10)
        {
            _engine = engine;
            _fuelTank = fuelTank;
            _fuelTankDisplay = fuelTankDisplay;
            _drivingInformationDisplay = drivingInformationDisplay;
            _drivingProcessor = drivingProcessor;           
            Acceleration = acceleration;
        }

        public void EngineStart()
        {
            _engine.Start();
        }

        public void EngineStop()
        {
            _engine.Stop();
        }

        public void Refuel(double liters)
        {
            _fuelTank.Refuel(liters);
        }

        public void RunningIdle()
        {
            _engine.Consume(FuelConsumption);
        }

        public void BrakeBy(int speed)
        {
            _drivingProcessor.ReduceSpeed(_drivingProcessor.ActualSpeed - speed);
        }

        public void Accelerate(int speed)
        {
            _drivingProcessor.IncreaseSpeedTo(_drivingProcessor.ActualSpeed + speed);
        }

        public void FreeWheel()
        {
            _drivingProcessor.ReduceSpeed(_drivingProcessor.ActualSpeed - 1);
        }
    }
}
