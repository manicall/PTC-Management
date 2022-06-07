
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
        public void Copy(T item, int Count)
        {

            var original = (T)GetSingle(item.Id);

            var temp = original.Clone();

            if (original is Itinerary)
            {
                (original as Itinerary).IdEmployee = (item as Itinerary).IdEmployee;
                (original as Itinerary).IdTransport = (item as Itinerary).IdTransport;
                (original as Itinerary).IdRoute = (item as Itinerary).IdRoute;

                (original as Itinerary).SetEntities();
            } 

            // Инициализация списка копий
            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)original.Clone()).ToList();

            set.AddRange(Items);



            db.SaveChanges();

            original.SetFields(temp);

            Update(original);
        }



        /// <summary>
        /// Присоединяет сущности в контекст
        /// </summary>
        private void Attach(T item) {
            if (item is Itinerary) (item as Itinerary).Attach(db);
            if (item is MaintanceLog) (item as MaintanceLog).Attach(db);
            if (item is LogOfDepartureAndEntry) (item as LogOfDepartureAndEntry).Attach(db);
        }

        /// <summary>
        /// Отсоединяет сущности от контекста
        /// </summary>
        private void Detach(T item)
        {
            if (item is Itinerary) (item as Itinerary).Detach(db);
            if (item is MaintanceLog) (item as MaintanceLog).Detach(db);
            if (item is LogOfDepartureAndEntry) (item as LogOfDepartureAndEntry).Detach(db);
        }

    }
}
