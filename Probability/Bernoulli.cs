using System;
using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    using SCU = StandardContinuousUniform;
    public sealed class Bernoulli : IDiscreteDistribution<int>
    {
        public static IDiscreteDistribution<int> Distribution(long zero, long one)
        {
            if (zero < 0 || one < 0)
                throw new ArgumentException();
            if (zero == 0 && one == 0)
                return Empty<int>.Distribution;
            if (zero == 0)
                return DiscreteSingleton<int>.Distribution(1);
            if (one == 0)
                return DiscreteSingleton<int>.Distribution(0);
            long gcd = Extensions.GCD(zero, one);
            return new Bernoulli(zero / gcd, one / gcd);
        }
        public long Zero { get; }
        public long One { get; }
        private Bernoulli(long zero, long one)
        {
            this.Zero = zero;
            this.One = one;
        }
        public int Sample() => (SCU.Distribution.Sample() <= ((double)Zero) / (Zero + One)) ? 0 : 1;
        public IEnumerable<int> Support() => Enumerable.Range(0, 2);
        public long Weight(int x) => x == 0 ? Zero : x == 1 ? One : 0;
        public override string ToString() => $"Bernoulli[{this.Zero}, {this.One}]";
    }
}

