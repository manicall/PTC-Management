using System.ComponentModel;

namespace PTC_Management.EntityFramework
{
    /// <summary>
    /// Позволяет использовать методы
    /// переопределенные в дочерних классах
    /// </summary>
    public abstract class Entity : IDataErrorInfo
    {
        public int? Id { get; set; }

        // методы интерфейса IDataErrorInfo
        public abstract string this[string columnName] { get; }

        public abstract string Error { get; }

        // методы для переопределения в дочерних классах
        public abstract void Add();

        public abstract void Update();

        public abstract bool Remove();

        public abstract void Copy(Entity selectedItem, int count);

        public abstract void SetFields(Entity entity);

        public abstract Entity Clone();

        protected Entity Clone<T>() where T : Entity, new()
        {
            var item = new T { Id = Id };
            item.SetFields(this);

            return item;
        }
    }
}
