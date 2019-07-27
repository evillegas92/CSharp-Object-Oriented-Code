using System;

namespace Module4Refactored
{
    public interface IPainter
    {
        bool IsAvailable { get; }
        TimeSpan EstimateTimeToPaint(double sqMeters);
        double EstimateCompensation(double sqMeters);
    }

    class ProportionalPainter : IPainter
    {
        public TimeSpan TimePerSquaredMeter { get; set; }
        public double DollarsPerHour { get; set; }

        public bool IsAvailable => true; //assume this painter is always available.

        public double EstimateCompensation(double sqMeters) =>
            this.EstimateTimeToPaint(sqMeters).TotalHours * this.DollarsPerHour;

        public TimeSpan EstimateTimeToPaint(double sqMeters) =>
            TimeSpan.FromHours(this.TimePerSquaredMeter.TotalHours * sqMeters);
    }
}
