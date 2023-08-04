using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class MarkovBuilder<T>
    {
        private DiscreteDistributionBuilder<T> initial = new DiscreteDistributionBuilder<T>();
        private Dictionary<T, DiscreteDistributionBuilder<T>> transitions = new Dictionary<T, DiscreteDistributionBuilder<T>>();

        public void AddInitial(T t)
        {
            initial.Add(t);
        }

        public void AddTransition(T t1, T t2)
        {
            if (!transitions.ContainsKey(t1))
                transitions[t1] = new DiscreteDistributionBuilder<T>();
            transitions[t1].Add(t2);
        }

        public Markov<T> ToDistribution()
        {
            var init = initial.ToDiscreteDistribution();
            var trans = transitions.ToDictionary(kv => kv.Key, kv => kv.Value.ToDiscreteDistribution());
            return Markov<T>.Distribution(init, k => trans.GetValueOrDefault(k, Empty<T>.Distribution));
        }
    }
}
