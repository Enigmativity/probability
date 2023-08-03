using System.Collections.Generic;
namespace Probability
{
    public interface IDiscreteDistribution<T> : IDistribution<T>
    {
        IEnumerable<T> Support();
        long Weight(T t);
    }
}
