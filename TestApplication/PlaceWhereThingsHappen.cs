using System;
using TestApplication.NeverClosedGenerics;

namespace TestApplication
{
    public class PlaceWhereThingsHappen<T, T1>
    {
        private IGenericInterface<int, string> thing1;
        private IGenericInterface<int, int> thing2;
        private IGenericInterface<string, int> thing3;
        private IGenericInterface<int, long> moreThing;

        private IGenericInterface<T, T1> thing4;

        private IGenericInterface<T, int> thing5;

        private IInterface anotherThingEntirely;

        public void thing() 
        {
            thing1.Method(1, "");
            thing2.Method(1, 1);
            thing3.Method("", 1);
            thing4.Method(default(T), default(T1));
            moreThing.Method(1, 2L);
            anotherThingEntirely.Method("", 1);

            thing3.Method("", 2);
            thing3.Method("", 3);
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