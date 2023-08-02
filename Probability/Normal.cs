﻿namespace Probability
{
    using static System.Math;
    using SCU = StandardContinuousUniform;
    public sealed class Normal : IWeightedDistribution<double>
    {
        public double Mean { get; }
        public double Sigma { get; }
        public double μ => Mean;
        public double σ => Sigma;
        public readonly static Normal Standard = Distribution(0, 1);
        public static Normal Distribution(double mean, double sigma) => new Normal(mean, sigma);
        private Normal(double mean, double sigma) => (this.Mean, this.Sigma) = (mean, sigma);
        // Box-Muller method
        private double StandardSample() => Sqrt(-2.0 * Log(SCU.Distribution.Sample())) * Cos(2.0 * PI * SCU.Distribution.Sample());
        public double Sample() => μ + σ * StandardSample();

        private static readonly double piroot = 1.0 / Sqrt(2 * PI);

        public double Weight(double x) => Exp(-(x - μ) * (x - μ) / (2 * σ  * σ)) * piroot / σ;
    }
}
