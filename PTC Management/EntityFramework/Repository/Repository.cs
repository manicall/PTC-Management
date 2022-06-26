using PTC_Management.Model;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Windows;
using System.Windows.Documents;

namespace PTC_Management.EntityFramework
{
    public class Repository<T> where T : Entity
    {
        private static readonly AppContext db = new AppContext();
        private readonly DbSet<T> set;

        static readonly string connectionString =
            ConfigurationManager
            .ConnectionStrings["PTC_ManagementConnection"].ConnectionString;

        public Repository() => set = db.Set<T>();

        public List<IGrouping<int, Date>> GetDates(DateTime dateTime) {
            var datesSet = db.Set<Date>();

            return datesSet.Where(item =>
                item.Date1.Month == dateTime.Month
                && item.Date1.Year == dateTime.Year).GroupBy(i => i.IdEmployee).ToList();
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
        /// 
        public List<T> GetMaintanceLogs(int idTransport, ViewModels viewModels)
        {
            List<T> list = set.Where(items => (items as MaintanceLog).Itinerary.Transport.Id == idTransport).ToList();

            return list;
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

            if (typeof(T) == typeof(Itinerary)) { 

                var list = set.Select(i => i).ToList();

                // todo нужно использовать один класс repository

                return list;
            }


            //var i = set.Select(item => item).ToList();

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

            return TrySaveChanges(item);
        }

        /// <summary>
        /// Изменяет запись в таблице базы данных
        /// </summary>
        public bool Update(T item)
        {
            SetEntities(item);

            if (item is Employee e) { 
                foreach (var i in e.Itinerary)
                    Console.WriteLine(i.Employee.GetFullName());
            }

            return TrySaveChanges(item);
        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        public bool Remove(T item)
        {
            //SetEntities(item);

            var dialogResult = MessageBox.Show(
                "Удалить запись?", "Удаление записи",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.No) return false;

            // отмечаем сущность как удаленную
            db.Entry(item).State = EntityState.Deleted;
            return TrySaveRemoveChanges(item);
           
        }

        public bool TrySaveRemoveChanges(T item) {
            try { db.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Прежде чем совершить удаление, необходимо удалить записи в других таблицах, " +
                    "которые используют выбранную для удаления запись",
                    "Ошибка удаления записи из базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                //отмена удаления
                db.Entry(item).State = EntityState.Unchanged;

                return false;
            }
            return true;
        }

        /// <summary>
        /// Выполняет удаление записи из базы данных
        /// </summary>
        public bool RemoveRange(List<List<T>> datesList, List<int> rowIndexes)
        {
            var dialogResult = MessageBox.Show(
                rowIndexes.Count > 1 ? "Удалить записи?" : "Удалить запись?", "Удаление записи",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.No) return false;

            for (int i = 0; i < rowIndexes.Count; i++)
            {
                set.RemoveRange(datesList[rowIndexes[i]]);
            }

            db.SaveChanges();
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

            // Инициализация списка копий
            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)selectedItem.Clone()).ToList();
            set.AddRange(Items);

            if (!TrySaveChanges(item)) return false;

            // возвращение selectedItem в исходное состояние
            selectedItem.SetFields(temp);
            Update((T)selectedItem);

            return true;
        }

        private bool TrySaveChanges(T item)
        {
            try { db.SaveChanges(); }
            catch (DbUpdateException ex)
            {
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
            if (item is Date) (item as Date).SetEntities();
            if (item is MaintanceLog) (item as MaintanceLog).SetEntities();
            if (item is LogOfDepartureAndEntry) (item as LogOfDepartureAndEntry).SetEntities();
        }
    }
}
