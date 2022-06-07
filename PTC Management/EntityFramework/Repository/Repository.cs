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
        public void Add(T item)
        {
            // если сущность имеет связные сущности,
            // то присоединяем их к контексту
            if (item is Itinerary) (item as Itinerary).SetEntities();

            // отмечаем сущность как добавленную
            db.Entry(item).State = EntityState.Added;

            db.SaveChanges();
        }

        /// <summary>
        /// Изменяет запись в таблице базы данных
        /// </summary>
        public void Update(T item)
        {
            // если сущность имеет связные сущности,
            // то присоединяем их к контексту
            if (item is Itinerary) (item as Itinerary).SetEntities();

            // отмечаем сущность как измененную
            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();

        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        public bool Remove(T item)
        {
            if (item is Itinerary) (item as Itinerary).SetEntities();

            // если сущность имеет связные сущности,
            // то отсоединяем их от контекста
            //Attach(item);

            // отмечаем сущность как удаленную
            db.Entry(item).State = EntityState.Deleted;

            // DONE: Обработать исключение, если сущность имеет связь
            try { db.SaveChanges(); }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    ex.InnerException.InnerException.Message,
                    "Ошибка удаления файла из базы данных",
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
        public void Copy(Entity selectedItem, T item, int Count)
        {
            /* для избежания ошибок связанных с несовместимостью 
             * объектов разных контекстов, необходимо использовать 
             * selectedItem для создания копий */
            var temp = selectedItem.Clone();

            CopyEntity(selectedItem, item);

            // Инициализация списка копий
            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)selectedItem.Clone()).ToList();

            set.AddRange(Items);
            db.SaveChanges();

            // возвращение selectedItem в исходное состояние
            selectedItem.SetFields(temp);
            Update((T)selectedItem);
        }

        private void CopyEntity(Entity selectedItem, T item)
        {
            selectedItem.SetFields(item);
            if (selectedItem is Itinerary) (selectedItem as Itinerary).SetEntities();
            if (selectedItem is MaintanceLog) (selectedItem as MaintanceLog).SetEntities();
            if (selectedItem is LogOfDepartureAndEntry) (selectedItem as LogOfDepartureAndEntry).SetEntities();

        }
    }
}
