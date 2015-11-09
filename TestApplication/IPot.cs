using System.Collections;
using System.Collections.Generic;

namespace TestApplication
{
    public interface IPot<T>
    {
        void Add(T item);
        void Find(string name);
    }

    public interface IPot<T, U>
    {
        void Add(T t, U u);
    }
}