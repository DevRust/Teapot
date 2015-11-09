using System;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class BasilAndThymePlant : BasilPlant<Thyme>
    {
        public void Method(Basil t1, string t2)
        {
            throw new NotImplementedException();
        }
    }
}