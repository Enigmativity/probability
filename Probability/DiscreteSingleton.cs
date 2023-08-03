using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class DiscreteSingleton<T> : IDiscreteDistribution<T>
    {
        private readonly T t;
        public static DiscreteSingleton<T> Distribution(T t) => new DiscreteSingleton<T>(t);
        private DiscreteSingleton(T t) => this.t = t;
        public T Sample() => t;
        public IEnumerable<T> Support() => Enumerable.Repeat(this.t, 1);
        public long Weight(T t) => EqualityComparer<T>.Default.Equals(this.t, t) ? 1 : 0;
        public override string ToString() => $"Singleton[{t}]";
    }
}
