using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace PTC_Management.EntityFramework
{
    public class Repository<T> where T : Entity
    {
        private readonly PTC_ManagementContext db;
        private readonly DbSet<T> set;

        public Repository(PTC_ManagementContext db)
        {
            this.db = db;
            set = db.Set<T>();
        }

        /// <summary>
        /// Возвращает запись из базы данных по заданному ключу и 
        /// вызывает исключение, если таких элементов больше одного.
        /// </summary>
        public T GetSingle(int id) => set.Single(item => item.Id == id);

        /// <summary>
        /// Возвращает запись из базы данных по заданному ключу и 
        /// вызывает исключение, если таких элементов больше одного.
        /// </summary>
        public TEntity GetSingle<TEntity>(int id) where TEntity : Entity
        {
            var set = db.Set<TEntity>();
            return set.Single(item => item.Id == id);
        }

        /// <summary>
        /// Возвращает набор записей, 
        /// значение ключей которых больше заданного ключа.
        /// </summary>
        public List<T> GetFrom(int id)
        {
            return set.Where(items => items.Id > id).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetMaintanceLogs(int idTransport)
        {
            return set.Where(items => (items as MaintanceLog).Itinerary.Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetLogOfDepartureAndEntry(int idTransport)
        {
            return set.Where(items => (items as LogOfDepartureAndEntry).Itinerary.Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Возвращает набор записей, которые содержат указанный id транспорта
        /// </summary>
        public List<T> GetItineraries(int idTransport)
        {
            return set.Where(items => (items as Itinerary).Transport.Id == idTransport).ToList();
        }

        /// <summary>
        /// Инициализация и возврат всех записей из таблицы.
        /// </summary>
        public List<T> GetList()
        {
            set.Load();
            return set.Select(item => item).ToList();
        }

        /// <summary>
        /// Добавляет запись в таблицу базы данных
        /// </summary>
        public bool Add(T item)
        {
            SetEntities(item);

            // отмечаем сущность как добавленную
            db.Entry(item).State = EntityState.Added;

            return TrySaveChanges();
        }

        /// <summary>
        /// Изменяет запись в таблице базы данных1
        /// </summary>
        public bool Update(T item)
        {
            SetEntities(item);

            // отмечаем сущность как измененную
            db.Entry(item).State = EntityState.Modified;

            return TrySaveChanges();
        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        public bool Remove(T item)
        {
            SetEntities(item);

            // отмечаем сущность как удаленную
            db.Entry(item).State = EntityState.Deleted;
            try { db.SaveChanges(); }
            catch (DbUpdateException)
            {
                MessageBox.Show(
                    "Прежде чем совершить удаление, необходимо удалить записи в других таблицах, " +
                    "которые используют выбранную для удаления запись",
                    "Ошибка удаления записи из базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                // отмена удаления
                db.Entry(item).State = EntityState.Unchanged;

                return false;
            }
            return true;
        }

        /// <summary>
        /// Выполняет добавление заданного числа копий в базу данных
        /// </summary>
        public bool Copy(Entity selectedItem, T item, int Count)
        {
            /* для избежания ошибок связанных с несовместимостью 
             * объектов разных контекстов, необходимо использовать 
             * selectedItem для создания копий */
            var temp = selectedItem.Clone();
            SetEntities(selectedItem, item);

            // todo делать номера маршрута уникальными при копировании
            //if (item is Route) {
            //    var route = set.Local.Max((i) => i);

            //    List<T> Routes = Enumerable.Range(1, Count).Select(i =>
            //    {
            //        var r = (T)selectedItem.Clone();
            //        (r as Route).Number = (route as Route).Number + i;
            //        return r;
            //    }).ToList();
            //    set.AddRange(Routes);
            //}

            // Инициализация списка копий
            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)selectedItem.Clone()).ToList();
            set.AddRange(Items);

            if (!TrySaveChanges()) return false;

            // возвращение selectedItem в исходное состояние
            selectedItem.SetFields(temp);
            Update((T)selectedItem);

            return true;
        }

        private bool TrySaveChanges()
        {
            try { db.SaveChanges(); }
            catch (DbUpdateException ex)
            {
                // сообщения с подробной информацией об ошибке
                // MessageBox.Show(
                //    ex.InnerException.InnerException.Message, caption,
                //    MessageBoxButton.OK, MessageBoxImage.Error);

                Console.WriteLine(ex.InnerException.InnerException.Message);

                return false;
            }
            return true;
        }

        /// <summary>
        /// Заполняет связанные сущности записями из базы данных
        /// </summary>
        private void SetEntities(Entity selectedItem, T item)
        {
            // заполняет поля значениями как у копии
            selectedItem.SetFields(item);
            SetEntities((T)selectedItem);
        }

        /// <summary>
        /// Заполняет связанные сущности записями из базы данных
        /// </summary>
        private void SetEntities(T item)
        {
            if (item is Itinerary) (item as Itinerary).SetEntities();
            if (item is MaintanceLog) (item as MaintanceLog).SetEntities();
            if (item is LogOfDepartureAndEntry) (item as LogOfDepartureAndEntry).SetEntities();
        }
    }
}
