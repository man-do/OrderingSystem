using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;

namespace OrderingSystem.Data.Data.Concrete
{
	public class DbRepository<T> : IRepository<T> where T : class
	{
		private OSEntities context = null;
		private DbSet<T> table = null;
		
		public DbRepository(OSEntities context)
		{
			this.context = context;
			table =  context.Set<T>();
		}

		public DbRepository()
		{
			context = new OSEntities();
			table = context.Set<T>();
		}

		public T GetById(object id)
		{
			T foundItem = table.Find(id);
			return foundItem;
		}
        public IEnumerable<T> GetAll(bool isAdmin = false)
        {
            if (isAdmin == true)
            {
                return table.Include("AspNetRoles");
            }

            return table;
        }
		public IEnumerable<T> GetAll() => table;

		public void Delete(object id)
		{
			T foundItem = table.Find(id);
			table.Remove(foundItem);
		}

		public void Insert(T obj)
		{
			table.Add(obj);
		}
	   
		public void Update(T obj)
		{
			table.Attach(obj);
			context.Entry(obj).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
