using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class RosemaryAndThymePlant : IPot<Rosemary, Thyme>
    {
        public void Add(Rosemary t, Thyme u)
        {
            throw new NotImplementedException();
        }
    }
}