﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace PTC_Management.EF
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly PTC_ManagementContext _db;
        private readonly DbSet<T> _Set;

        public static bool AutoSaveChanges { get; set; } = true;

        public Repository(PTC_ManagementContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _Set;

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);
        public T GetLast(int id) => Items.LastOrDefault(item => item.Id == id);

        public List<T> GetAll() {
            return Items.DefaultIfEmpty().ToList();
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;


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

        public void Remove(int id)
        {
            var item = Get(id);
            if (item is null) return;
            _db.Entry(item).State = EntityState.Deleted;

            if (AutoSaveChanges)
                _db.SaveChanges();
        }
    }
}
