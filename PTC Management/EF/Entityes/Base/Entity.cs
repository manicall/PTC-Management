using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.EF
{
    public class Entity
    {
        public int Id { get; set; }

        // методы созданы для переопределения в дочерних классах
        public virtual void Add()
        {
            throw new NotImplementedException();
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        public virtual void Remove()
        {
            throw new NotImplementedException();
        }

        public virtual void Copy(int count)
        {
            throw new NotImplementedException();
        }

        public virtual void SetFields(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Entity Clone()
        {
            throw new NotImplementedException();
        }
    }
}
