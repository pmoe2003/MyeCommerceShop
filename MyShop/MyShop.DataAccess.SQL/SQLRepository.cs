using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    //The IRepository generated error that it wasn't seeing the Member class - 
    //So you must implement the interface to create boiler plates for each of the methods
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext context; //from the DataContext class in MyShop.DataAccess.SQL
        internal DbSet<T> dbSet; //underlying table that we want to access        

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>(); //pass in the model we want to work with which is T
                                           //being either Products or ProductCategories
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        
        }

        public void Commit()
        {
            context.SaveChanges();
         
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            //First Find the Id - then if found and detached then attach it to the underlying framework
            //then it can be deleted
            if(context.Entry(t).State == EntityState.Detached)
            dbSet.Attach(t);

            dbSet.Remove(t);
            
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
           
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
            
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
         
        }
    }
}
