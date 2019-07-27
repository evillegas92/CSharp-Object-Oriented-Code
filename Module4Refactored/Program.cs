using System;
using System.Collections;
using System.Collections.Generic;

namespace Module4Refactored
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<ProportionalPainter> painters = new ProportionalPainter[10];

            IPainter painter = CompositePainterFactories.CreateGroup(painters);
        }

        private static IPainter FindCheapestPainter(double sqMeters, Painters painters) =>
            painters.GetAvailable().GetCheapestOne(sqMeters);

        private static IPainter FindFastestPainter(double sqMeters, Painters painters) =>
            painters.GetAvailable().GetFastestOne(sqMeters);
    }
}