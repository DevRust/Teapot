using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public interface IGenericInterface<T1, T2>
    {
        void Method(T1 t1, T2 t2);
    }

    public interface IInterface
    {
        void Method(string t1, int t2);
    }

    public class Instance1 : IGenericInterface<int, string>
    {
        public void Method(int t1, string t2)
        {
            throw new NotImplementedException();
        }
    }
    public class Instance2 : IGenericInterface<int, int>
    {
        public void Method(int t1, int t2)
        {
            throw new NotImplementedException();
        }
    }

    public class Instance3 : IGenericInterface<string, int>
    {
        public void Method(string t1, int t2)
        {
            throw new NotImplementedException();
        }
    }

    public class Instance4<T1> : IGenericInterface<int, T1>
    {
        public virtual void Method(int t1, T1 t2)
        {
            throw new NotImplementedException();
        }
    }

    public class Instance5 : Instance4<long>
    {
    }

    public class Instance6<T1, T2> : IGenericInterface<T1, T2>
    {
        public void Method(T1 t1, T2 t2)
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
}
