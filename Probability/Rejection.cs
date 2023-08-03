﻿using System;
namespace Probability
{
    using static Distribution;
    public class Rejection<T> : IContinuousDistribution<T>
    {
        private readonly Func<T, double> weight;
        private readonly IContinuousDistribution<T> dominating;
        private readonly double factor;

        public static IContinuousDistribution<T> Distribution(Func<T, double> weight, IContinuousDistribution<T> dominating, double factor = 1.0) =>
            new Rejection<T>(weight, dominating, factor);

        private Rejection(Func<T, double> weight, IContinuousDistribution<T> dominating, double factor)
        {
            this.weight = weight;
            this.dominating = dominating;
            this.factor = factor;
        }

        public T Sample()
        {
            while (true)
            {
                T t = this.dominating.Sample();
                double dw = this.dominating.Weight(t) * this.factor;
                double w = this.weight(t);
                if (BooleanBernoulli(w / dw).Sample())
                    return t;
            }
        }

        public double Weight(T t) => weight(t);
    }
}
