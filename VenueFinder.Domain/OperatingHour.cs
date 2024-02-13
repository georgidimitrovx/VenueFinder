namespace VenueFinder.Domain
{
    public class OperatingHour
    {
        public DayOfWeek Day { get; private set; }
        public TimeSpan OpenTime { get; private set; }
        public TimeSpan CloseTime { get; private set; }

        // Constructor and methods
    }
}
