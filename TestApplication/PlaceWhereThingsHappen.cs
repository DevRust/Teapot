using System;
using TestApplication.Entities;
using TestApplication.NeverClosedGenerics;

namespace TestApplication
{
    public class PlaceWhereThingsHappen<T, T1>
    {
        private IPot<Rosemary, Thyme> thing1;
        private IPot<Basil, Thyme> thing2;
        private IPot<Basil, Rosemary> moreThing;
        private IPot<Chives, Basil> thing3;

        private IPot<T, T1> thing4;

        private IPot<T, Basil> thing5;

        private IInterface anotherThingEntirely;

        private IPot<T> teapot;

        public void thing() 
        {
            thing1.Add(new Rosemary(), new Thyme());
            thing2.Add(new Basil(), new Thyme());
            thing3.Add(new Chives(), new Basil());
            thing4.Add(default(T), default(T1));
            moreThing.Add(new Basil(), new Rosemary());
            anotherThingEntirely.Method("", 1);

            thing3.Add(new Chives(), new Basil());
            thing3.Add(new Chives(), new Basil());
            anotherThingEntirely.Method("", 1);
        }

        public void NeverImplemented()
        {
            var opt = new Option<Guid>(Guid.NewGuid());
            opt.Some();

            var opt2 = new Option<bool>(true);
            opt2.Some();

            var opt3 = new Option<bool>(false);
            opt3.Some();
        }
    }
}