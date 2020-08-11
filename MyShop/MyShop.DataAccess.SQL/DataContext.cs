using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //Create a constructor to capture connectionstring that the
        //base class is expecting
        public DataContext()
            : base("DefaultConnection")
        {

        }

        //Create constructor to identity what model will be stored in the database
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
