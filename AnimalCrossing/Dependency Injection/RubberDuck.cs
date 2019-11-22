using System;
namespace AnimalCrossing.DependencyInjection
{
    public class RubberDuck : Duck
    {
        public RubberDuck(IFlyBehavior fly) : base(fly)
        {
        }

        public override void Display()
        {
            Console.WriteLine("I am a rubber duck");
        }
    }
}
