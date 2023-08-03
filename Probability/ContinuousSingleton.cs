using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class ContinuousSingleton<T> : IContinuousDistribution<T>
    {
        private readonly T t;
        public static ContinuousSingleton<T> Distribution(T t) => new ContinuousSingleton<T>(t);
        private ContinuousSingleton(T t) => this.t = t;
        public T Sample() => t;
        public double Weight(T t) => EqualityComparer<T>.Default.Equals(this.t, t) ? 1 : 0;
        public override string ToString() => $"Singleton[{t}]";
    }
}
