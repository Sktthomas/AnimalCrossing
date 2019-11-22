using System;
namespace AnimalCrossing.DependencyInjection
{
    public class FlyNoWay : IFlyBehavior
    {
        public FlyNoWay()
        {
        }

        public void Fly()
        {
            Console.WriteLine("I can't fly, *sobs* ");
        }
    }
}
