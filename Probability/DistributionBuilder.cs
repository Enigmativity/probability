using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class DistributionBuilder<T>
    {
        private Dictionary<T, long> weights = new Dictionary<T, long>();

        public void Add(T t)
        {
            weights[t] = weights.GetValueOrDefault(t) + 1;
        }

        public IDistribution<T> ToDistribution()
        {
            var keys = weights.Keys.ToList();
            var values = keys.Select(k => weights[k]);
            return keys.ToWeighted(values);
        }
    }
}
