using PTC_Management.ViewModel.Base;

using System.ComponentModel;

namespace PTC_Management.EF
{
    /// <summary>
    /// Позволяет использовать методы
    /// переопределенные в дочерних классах
    /// </summary>
    
    // TODO: доделать валидацию
    public abstract class Entity : IDataErrorInfo
    {
        /* Данное поле следует удалить в дочерних классах,
         * так как при выполнении метода GetSingle,
         * Id переменной DialogItem равно 0 (так как изменяется не правильный id),
         * что приводит к некорректному поведению программы
         */
        public int Id { get; set; }
        public abstract string Error { get; }

        public abstract string this[string columnName] { get; }

        // методы созданы для переопределения в дочерних классах
        public abstract void Add();

        public abstract void Update();

        public abstract bool Remove();

        public abstract void Copy(int count);

        public abstract void SetFields(Entity entity);

        public abstract Entity Clone();
    }
}
