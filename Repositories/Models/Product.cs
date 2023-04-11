using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }
        public int? Price { get; set; }
        public string? ImageName { get; set; }
        //public virtual Producer? Producer { get; set; }
       
        
        public Product()
        {

        }

    }
}
