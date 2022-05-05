using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.EF
{
    public interface IRepository<T> where T : class, new()
    {

        IQueryable<T> Items { get; }

        T Get(int id);

        T GetLast(int id);

        List<T> GetAll();

        void Add(T item);

        void Update(T item);
        
        void Remove(int id);

    }
}