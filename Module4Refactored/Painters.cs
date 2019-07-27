using System.Collections.Generic;
using System.Linq;

namespace Module4Refactored
{
    class Painters
    {
        private IEnumerable<IPainter> ContainedPainters { get; }

        public Painters(IEnumerable<IPainter> painters)
        {
            this.ContainedPainters = painters.ToList();
        }

        public Painters GetAvailable()
        {
            if (this.ContainedPainters.All(painter => painter.IsAvailable))
                return this;
            return new Painters(this.ContainedPainters.Where(painter => painter.IsAvailable));
        }

        public IPainter GetCheapestOne(double squaredMeters) =>
            this.ContainedPainters.WithMinimum(painter => painter.EstimateCompensation(squaredMeters));

        public IPainter GetFastestOne(double squaredMeters) =>
            this.ContainedPainters.WithMinimum(painter => painter.EstimateTimeToPaint(squaredMeters));
    }
}