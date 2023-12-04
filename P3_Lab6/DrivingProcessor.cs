using Microsoft.VisualBasic.FileIO;

namespace CarBuilder
{
    public class DrivingProcessor : IDrivingProcessor
    {
        public int ActualSpeed { get; set; } = 0;

        public void IncreaseSpeedTo(int speed)
        {
            speed = speed > 250 ? 250 : speed;
            ActualSpeed = speed;
        }

        public void ReduceSpeed(int speed)
        {
            speed = speed < 0 ? 0 : speed;
            speed = ActualSpeed - speed > 10 ? ActualSpeed - 10 : speed;
            ActualSpeed = speed;
        }
    }
}
