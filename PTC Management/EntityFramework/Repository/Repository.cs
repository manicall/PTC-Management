
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace PTC_Management.EF
{
    public class Repository<T> where T : Entity
    {
        private readonly PTC_ManagementContext _db;
        private readonly DbSet<T> _set;

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
        public T GetSingle(int id) => Items.Single(item => item.Id == id);

        /// <summary>
        /// Возвращает набор записей, 
        /// значение ключей которых больше заданного ключа.
        /// </summary>
        public List<T> GetFrom(int id)
        {
            return Items.Where(items => items.Id > id).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetMaintanceLogs(int idTransport)
        {
            return Items.Where(items => (items as MaintanceLog).Itinerary.Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetLogOfDepartureAndEntry(int idTransport)
        {
            return Items.Where(items => (items as LogOfDepartureAndEntry).Itinerary.Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetItineraries(int idTransport)
        {
            return Items.Where(items => (items as Itinerary).Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Инициализация и возврат всех записей из таблицы.
        /// </summary>
        public List<T> GetList()
        {
            _set.Load();
            return _set.Local.ToList();
        }

        /// <summary>
        /// Добавляет запись в таблицу базы данных
        /// </summary>
        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _set.Add(item);

            _db.SaveChanges();
        }

        /// <summary>
        /// Изменяет запись в таблице базы данных
        /// </summary>
        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;

            _db.SaveChanges();
        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        public bool Remove(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Deleted;

            // DONE: Обработать исключение, если сущность имеет связь
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message,
                    "Ошибка удаления файла из базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                // отмена удаления
                _db.Entry(item).State = EntityState.Unchanged;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Выполняет добавление заданного числа копий в базу данных
        /// </summary>
        public void Copy(T item, int Count)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            // Инициализация списка копий
            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)item.Clone()).ToList();

            _set.AddRange(Items);

            _db.SaveChanges();
        }

    }
}
