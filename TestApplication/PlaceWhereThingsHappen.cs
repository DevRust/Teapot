using System;

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

        public void thing() 
        {
            thing1.Method(1, "");
            thing2.Method(1, 1);
            thing3.Method("", 1);
            thing4.Method(default(T), default(T1));
            moreThing.Method(1, 2L);
        }

    }
}