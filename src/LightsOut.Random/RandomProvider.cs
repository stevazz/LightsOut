using LightsOut.Random.Interfaces;

namespace LightsOut.Random
{
    public class RandomProvider : IRandomProvider
    {

        private readonly System.Random random;

        public RandomProvider()
        {
            random = new System.Random();
        }

        public double Next()
        {
            return random.NextDouble();
        }
    }
}
