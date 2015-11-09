using System;
using TestApplication.Entities;

namespace TestApplication
{
    public class OpenGeneric<T1, T2> : IPot<T1, T2>
    {
        public void Add(T1 t1, T2 t2)
        {
            throw new NotImplementedException();
        }
    }

    public class Instance7 : IInterface
    {
        public void Method(string t1, int t2)
        {
            throw new NotImplementedException();
        }
    }

    public class BasilPlant<T2> : IPot<Basil, T2>
    {
        public void Add(Basil t, T2 u)
        {
            throw new NotImplementedException();
        }
    }
}
    