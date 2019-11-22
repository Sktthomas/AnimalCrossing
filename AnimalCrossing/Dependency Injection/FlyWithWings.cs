using System;
namespace AnimalCrossing.DependencyInjection
{
    public class FlyWithWings : IFlyBehavior
    {
        public FlyWithWings()
        {
        }

        public void Fly()
        {
            Console.WriteLine("I am flying");
        }
    }
}
