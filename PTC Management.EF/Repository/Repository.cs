using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PTC_Management.EF
{
    public class Repository<T> where T : Entity
    {
        private readonly PTC_ManagementContext _db;
        private readonly DbSet<T> _set;

        /// <summary> Определяет будут ли выполняться
        /// сохранения в базе данных автоматически </summary>
        public static bool AutoSaveChanges { get; set; } = true;

        public Repository(PTC_ManagementContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _set;
        /// <summary>
        /// Возвращает запись из базы данных по заданному ключу и 
        /// вызывает исключение, если таких элементов больше одного.
        /// </summary>
        /// <param name="id">Ключ, по которому производится поиск.</param>
        /// <returns>Запись в базе данных по заданному ключу.</returns>
        public T GetSingle(int id) => Items.Single(item => item.Id == id);

        /// <summary>
        /// Возвращает набор записей, 
        /// значение ключей которых больше заданного ключа.
        /// </summary>
        /// <param name="id">Ключ, по которому производится поиск.</param>
        /// <returns> Список записей из таблицы базы данных. </returns>
        public List<T> GetFrom(int id)
        {
            return Items.Where(items => items.Id > id).ToList();
        }

        /// <summary>
        /// Инициализация и возврат всех записей из таблицы.
        /// </summary>
        /// <returns> Все записи из таблицы. </returns>
        public ObservableCollection<T> GetObservableCollection()
        {
            _set.Load();
            return _set.Local;
        }

        /// <summary>
        /// Добавляет запись в таблицу базы данных
        /// </summary>
        /// <param name="item"> Добавляемое значение. </param>
        /// <exception cref="ArgumentNullException">
        /// Если аргумент имеет null значение.
        /// </exception>
        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _set.Add(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        /// <summary>
        /// Изменяет запись в таблице базы данных
        /// </summary>
        /// <param name="item"> Изменяемое значение. </param>
        /// <exception cref="ArgumentNullException">
        /// Если аргумент имеет null значение.
        /// </exception>
        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        /// <param name="id">Ключ, по которому происходит
        /// поиск записи в таблице</param>
        /// <exception cref="ArgumentNullException">
        /// Если аргумент имеет null значение.
        /// </exception>
        public void Remove(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Deleted;

            // TODO: Обработать исключение, если сущность имеет связь
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        /// <summary>
        /// Выполняет добавление заданного числа копий в базу данных
        /// </summary>
        /// <param name="item">Копируемый объект</param>
        /// <param name="Count">
        /// Число копий, которые будут добавленны в базу данных
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Если аргумент имеет null значение.
        /// </exception>
        public void Copy(T item, int Count)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            // Инициализация списка копий
            List<T> Items = Enumerable
                .Range(1, Count).Select(i => (T)item.Clone()).ToList();

            _set.AddRange(Items);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

    }
}
