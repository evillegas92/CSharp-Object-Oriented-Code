using System;
using System.Collections.Generic;
using System.Linq;

namespace Module4Refactored
{
    static class CompositePainterFactories
    {
        public static IPainter CreateCheapestSelector(IEnumerable<IPainter> painters) =>
            new CompositePainter<IPainter>(painters,
                (sqMeters, sequence) => new Painters(sequence).GetAvailable().GetCheapestOne(sqMeters));

        public static IPainter CreateFastestSelector(IEnumerable<IPainter> painters) =>
            new CompositePainter<IPainter>(painters,
                (sqMeters, sequence) => new Painters(sequence).GetAvailable().GetFastestOne(sqMeters));

        public static IPainter CreateGroup(IEnumerable<ProportionalPainter> painters) =>
            new CompositePainter<ProportionalPainter>(painters, (sqMeters, sequence) => 
            {
                TimeSpan time = TimeSpan.FromHours(
                1 / sequence
                .Where(painter => painter.IsAvailable)
                .Select(painter => 1 / painter.EstimateTimeToPaint(sqMeters).TotalHours)
                .Sum() /*velocity of all painters.*/);
                //reciprocal of the velocity total is the total number of hours it will take all painters.

                double totalCost = sequence
                    .Where(painter => painter.IsAvailable)
                    .Select(painter => painter.EstimateCompensation(sqMeters) / painter.EstimateTimeToPaint(sqMeters).TotalHours * time.TotalHours)
                    .Sum();

                //now we know how much time and how much money it will take if we let all the painters work together.
                return new ProportionalPainter
                {
                    TimePerSquaredMeter = TimeSpan.FromHours(time.TotalHours / sqMeters),
                    DollarsPerHour = totalCost / time.TotalHours
                };
            });
    }
}