using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBuilder
{
    public class FuelTank : IFuelTank
    {
        public double FillLevel => fillLevel;
        private double fillLevel = 20;
        public bool IsOnReserve => isOnReserve;
        private bool isOnReserve = false;
        public void Consume(double liters)
        {
            fillLevel -= liters;
            if (fillLevel < 5)
            {
                isOnReserve = true;
                if (fillLevel < 0)
                {
                    fillLevel = 0;
                    Console.WriteLine("Bak jest pusty");
                }
            }
        }

        public void Refuel(double liters)
        {           
            fillLevel += liters;
            if (fillLevel > 5)
            {
                isOnReserve |= false;
                if (fillLevel > 60)
                {
                    fillLevel = 60;
                    Console.WriteLine("Bak jest pełen");
                }
            }
            
        }
    }
}
