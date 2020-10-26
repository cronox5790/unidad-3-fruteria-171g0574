using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitStore.Models;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace FruitStore.Repositories
{
    public class Repository<T> where T:class
    {
       public fruteriashopContext Context { get; set; }

        public Repository(fruteriashopContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual T Get(object id)
        {
            return Context.Find<T>(id);  
        }

        public virtual void Insert(T entidad)
        {
            if (validate(entidad))
            {
                Context.Add<T>(entidad);
                Context.SaveChanges();
            }
        }

        public virtual void Update(T entidad)
        {
            if (validate(entidad))
            {
                Context.Update<T>(entidad);
                Context.SaveChanges();
            }
        }

        public virtual void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }
        public virtual  bool validate(T entidad)
        {
            return true;
        }
    }
}
