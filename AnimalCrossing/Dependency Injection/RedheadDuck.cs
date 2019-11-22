using System;
namespace AnimalCrossing.DependencyInjection
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck(IFlyBehavior fly) : base(fly)
        {
        }

        public override void Display()
        {
            Console.WriteLine("I am a redhead duck, looking nice by the way");
        }
    }
}
