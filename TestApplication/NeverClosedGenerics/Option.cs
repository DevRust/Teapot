using System;

namespace TestApplication.NeverClosedGenerics
{
    public class Option<T>
    {
        public Option(T value)
        {
            this.value = value;
        }

        private readonly T value;

        public T Value
        {
            get
            {
                if (value == null) throw new Exception();
                return value;
            }
        }

        public bool Some()
        {
            return value != null;
        }
    }
}