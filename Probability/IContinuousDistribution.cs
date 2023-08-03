namespace Probability
{
    public interface IContinuousDistribution<T> : IDistribution<T>
    {
        double Weight(T t);
    }
}
