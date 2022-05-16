using System;

namespace PTC_Management.EF
{
    /// <summary>
    /// Позволяет использовать методы
    /// переопределенные в дочерних классах
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }

        // методы созданы для переопределения в дочерних классах
        public abstract void Add();

        public abstract void Update();

        public abstract void Remove();

        public abstract void Copy(int count);

        public abstract void SetFields(Entity entity);

        public abstract Entity Clone();
    }
}
