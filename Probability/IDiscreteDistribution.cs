using System.Collections.Generic;
namespace Probability
{
    public interface IDiscreteDistribution<T> : IWeightedDistribution<T>
    {
        IEnumerable<T> Support();
        new long Weight(T t);
    }
}
