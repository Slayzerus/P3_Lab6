using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBuilder
{
    public interface IFuelTank
    {
        double FillLevel { get; }
        bool IsOnReserve { get; }
        void Consume(double liters);
        void Refuel(double liters);
    }
}
