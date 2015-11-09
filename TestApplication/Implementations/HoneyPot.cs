using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class HoneyPot : IPot<Honey>
    {
        public void Add(Honey item)
        {
            throw new NotImplementedException();
        }

        public void Find(string name)
        {
            throw new NotImplementedException();
        }
    }
}