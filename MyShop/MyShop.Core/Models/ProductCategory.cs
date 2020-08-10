using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    //Product is implemented by BaseEntity
    public class ProductCategory : BaseEntity
    {
        //Got rid of this because Id is in the baseEntity
        //public string Id { get; set; }
        public string Category { get; set; }

        //Got rid of the constructor because this is now handled in the BaseEntity
        //create new id everytime a category model is built
        //public ProductCategory()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}

    }


}
