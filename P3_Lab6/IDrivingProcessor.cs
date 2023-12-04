namespace CarBuilder
{
    public interface IDrivingProcessor // car #2
    {
        int ActualSpeed { get; set; }
        void IncreaseSpeedTo(int speed);
        void ReduceSpeed(int speed);
    }
}
