using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class BasilAndBeerPlant : IPot<Basil, Beer>
    {
        public void Add(Basil t1, Beer t2)
        {
            throw new NotImplementedException();
        }
    }
}