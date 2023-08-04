using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class DiscreteDistributionBuilder<T>
    {
        private Dictionary<T, long> weights = new Dictionary<T, long>();

        public void Add(T t)
        {
            weights[t] = weights.GetValueOrDefault(t) + 1;
        }

        public IDiscreteDistribution<T> ToDiscreteDistribution()
        {
            var keys = weights.Keys.ToList();
            var values = keys.Select(k => weights[k]);
            return keys.ToWeighted(values);
        }
    }
}
