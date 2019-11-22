using System;
namespace AnimalCrossing.DependencyInjection
{
    public class MallardDuck : Duck
    {
        public MallardDuck(IFlyBehavior fly) : base(fly)
        {
        }

        public override void Display()
        {
            Console.WriteLine("I am a mallard duck");
        }
    }
}
