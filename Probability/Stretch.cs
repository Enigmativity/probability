namespace Probability
{
    public sealed class Stretch : IContinuousDistribution<double>
    {
        private IContinuousDistribution<double> d;
        private double shift;
        private double stretch;

        public static IContinuousDistribution<double> Distribution(IContinuousDistribution<double> d, double stretch, double shift = 0.0, double around = 0.0)
        {
            if (stretch == 1.0 && shift == 0.0) return d;
            return new Stretch(d, stretch, shift + around - around * stretch);
        }
        private Stretch(IContinuousDistribution<double> d, double stretch, double shift)
        {
            this.d = d;
            this.stretch = stretch;
            this.shift = shift;
        }
        public double Sample() => d.Sample() * stretch + shift;
        // Dividing the weight by stretch preserves the normalization constant 
        public double Weight(double x) => d.Weight((x - shift) / stretch) / stretch;
    }
}
