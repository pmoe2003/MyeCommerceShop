using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {

        public string Id { get; set; }
        public string Category { get; set; }

        //create new id everytime a category model is built
        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
            
    }


}
