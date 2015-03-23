namespace TestApplication
{
    public class PlaceWhereThingsHappen
    {
        private IGenericInterface<int, string> thing1;
        private IGenericInterface<int, int> thing2;
        private IGenericInterface<string, int> thing3;
        private IGenericInterface<int, long> moreThing;

        public void thing()
        {
            thing1.Method(1, "");
            thing2.Method(1, 1);
            thing3.Method("", 1);
            moreThing.Method(1, 2L);
        }

    }
}