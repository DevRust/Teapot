using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class PotOfBeer : IPot<Beer>
    {
        public void Add(Beer item)
        {
            throw new NotImplementedException();
        }

        public void Find(string name)
        {
            throw new NotImplementedException();
        }
    }
}