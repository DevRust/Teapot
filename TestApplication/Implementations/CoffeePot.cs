using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class CoffeePot : IPot<Coffee>
    {
        public void Add(Coffee item)
        {
            throw new NotImplementedException();
        }

        public void Find(string name)
        {
            throw new NotImplementedException();
        }
    }
}