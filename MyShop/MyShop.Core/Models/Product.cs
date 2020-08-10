using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    //Product is implemented by BaseEntity
    public class Product : BaseEntity
    {

        //Got rid of this because Id is in the baseEntity
        //public string Id { get; set; }
        
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }


        //Got rid of the constructor because this is now handled in the BaseEntity
        //public Product()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
