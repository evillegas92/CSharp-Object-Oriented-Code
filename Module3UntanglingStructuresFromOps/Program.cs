using System;
using System.Collections.Generic;
using System.Linq;

namespace Module4UntanglingStructuresFromOps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static IPainter FindCheapestPainter(double sqMeters, IEnumerable<IPainter> painters)
        {
            return painters
                    .Where(painter => painter.IsAvailable)
                    .WithMinimum(painter => painter.EstimateCompensation(sqMeters));
        }

        private static IPainter FindFastestPainter(double sqMeters, IEnumerable<IPainter> painters)
        {
            return painters
                    .Where(painter => painter.IsAvailable)
                    .WithMinimum(painter => painter.EstimateTimeToPaint(sqMeters));
        }

        /* Algorithm outline:
         * 1. Each worker paints part of the wall.
         * 2. Calculate time for entire work.
         * 3. Let each painter work that long.
         * 4. Their total output will be equal to sqMeters.
         * Assumption: painters work at constant speed.
         * Assumption: money paid to a painter is proportional to time spent painting.
         */
        private static IPainter Worktogether(double sqMeters, IEnumerable<IPainter> painters)
        {
            TimeSpan time = TimeSpan.FromHours(
                1 / painters
                .Where(painter => painter.IsAvailable)
                .Select(painter => 1 / painter.EstimateTimeToPaint(sqMeters).TotalHours)
                .Sum() /*velocity of all painters.*/);
            //reciprocal of the velocity total is the total number of hours it will take all painters.

            double totalCost = painters
                .Where(painter => painter.IsAvailable)
                .Select(painter => painter.EstimateCompensation(sqMeters) / painter.EstimateTimeToPaint(sqMeters).TotalHours * time.TotalHours)
                .Sum();

            //now we know how much time and how much money it will take if we let all the painters work together.
            return new ProportionalPainter
            {
                TimePerSquaredMeter = TimeSpan.FromHours(time.TotalHours / sqMeters),
                DollarsPerHour = totalCost / time.TotalHours
            };
        }
    }
}