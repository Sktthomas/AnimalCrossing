using System;
namespace AnimalCrossing.DependencyInjection
{
    public abstract class Duck
    {
        IFlyBehavior fb;

        public void Fly()
        {
            this.fb.Fly();
        }

        public void Quack()
        {
            Console.WriteLine("Quack");
        }

        public abstract void Display();

        public Duck(IFlyBehavior fly)
        {
            this.fb = fly;
        }
    }
}
