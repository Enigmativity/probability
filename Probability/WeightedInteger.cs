using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    using SDU = StandardDiscreteUniform;
    // Weighted integer distribution using alias method.
    public sealed class WeightedInteger : IDiscreteDistribution<int>
    {
        private readonly List<long> weights;
        private readonly IDistribution<int>[] distributions;
        private readonly IDistribution<int> distribution = null;

        public static IDiscreteDistribution<int> Distribution(params long[] weights) =>
            Distribution((IEnumerable<long>)weights);

        public static IDiscreteDistribution<int> Distribution(IEnumerable<long> weights)
        {
            List<long> w = weights.ToList();
            if (w.Any(x => x < 0))
                throw new ArgumentException();
            if (!w.Any(x => x > 0))
                return Empty<int>.Distribution;
            if (w.Count == 1)
                return DiscreteSingleton<int>.Distribution(0);
            if (w.Count == 2)
                return Bernoulli.Distribution(w[0], w[1]);
            long gcd = weights.GCD();
            for (int i = 0; i < w.Count; i += 1)
                w[i] /= gcd;
            return new WeightedInteger(w);
        }

        private WeightedInteger(IEnumerable<long> weights)
        {
            this.weights = weights.ToList();
            long s = this.weights.Sum();
            int n = this.weights.Count;
            this.distributions = new IDistribution<int>[n];
            var lows = new Dictionary<int, long>();
            var highs = new Dictionary<int, long>();
            for (int i = 0; i < n; i += 1)
            {
                long w = this.weights[i] * n;
                if (w == s)
                    this.distributions[i] = DiscreteSingleton<int>.Distribution(i);
                else if (w < s)
                    lows.Add(i, w);
                else
                    highs.Add(i, w);
            }
            while (lows.Any())
            {
                var low = lows.First();
                lows.Remove(low.Key);
                var high = highs.First();
                highs.Remove(high.Key);
                long lowNeeds = s - low.Value;
                this.distributions[low.Key] =
                    Bernoulli.Distribution(low.Value, lowNeeds)
                      .Select(x => x == 0 ? low.Key : high.Key);
                long newHigh = high.Value - lowNeeds;
                if (newHigh == s)
                    this.distributions[high.Key] =
                      DiscreteSingleton<int>.Distribution(high.Key);
                else if (newHigh < s)
                    lows[high.Key] = newHigh;
                else
                    highs[high.Key] = newHigh;
            }
            distribution = SDU.Distribution(0, this.weights.Count - 1);
        }

        public IEnumerable<int> Support() => Enumerable.Range(0, weights.Count).Where(x => weights[x] != 0);

        public long Weight(int i) => 0 <= i && i < weights.Count ? weights[i] : 0;

        public int Sample()
        {
            int i = distribution.Sample();
            return distributions[i].Sample();
        }

        public override string ToString() => $"WeightedInteger[{weights.CommaSeparated()}]";
    }
}
