using System;
using System.Collections.Generic;
using TestApplication.Entities;

namespace TestApplication.Implementations
{
    public class TeaPot : IPot<Tea>
    {
        public TeaPot()
        {
            Items = new List<Tea>();
        }
        
        public void Add(Tea item)
        {
            Items.Add(item);

            if (this.IsAllSteamedUp)
            {
                PourMeOut();
            }
        }

        public ICollection<Tea> Items { get; }
        public void Find(string name)
        {
            throw new NotImplementedException();
        }

        private void PourMeOut()
        {
            throw new NotImplementedException();
        }

        public bool IsAllSteamedUp { get; set; }
    }
}