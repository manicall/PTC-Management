using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PTC_Management.EF
{
    public class Repository<T> where T : Entity, new()
    {
        private readonly PTC_ManagementContext _db;
        private readonly DbSet<T> _set;

        public static bool AutoSaveChanges { get; set; } = true;

        public Repository(PTC_ManagementContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _set;

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);
        public T GetLast(int id) => Items.LastOrDefault(item => item.Id == id);

        public List<T> GetFrom(int id) => Items.Where(items => items.Id > id).ToList();

        public ObservableCollection<T> GetAll() {
            _set.Load();
            return _set.Local;
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _set.Add(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }



        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;

            if (AutoSaveChanges)
                _db.SaveChanges();

        }

        /// <summary>
        /// Выполняет удаление из базы данных
        /// </summary>
        /// <param name="id">Ключ, по которому происходит
        /// поиск записи в таблице</param>
        public void Remove(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var newItem = Get(item.Id);
            _db.Entry(newItem).State = EntityState.Deleted;

            // TODO: Обработать исключение, если сущность имеет связь
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public void Copy(T item, int Count) {
            if (item is null) throw new ArgumentNullException(nameof(item));

            List<T> Items = Enumerable.Range(1, Count).Select(i => (T)item.Clone()).ToList();

            Console.WriteLine(Items);

            _set.AddRange(Items);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

 

    }
}
