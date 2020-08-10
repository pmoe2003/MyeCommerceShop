using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {

        public Product Product { get; set; }

        //Itererate through for product category
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
