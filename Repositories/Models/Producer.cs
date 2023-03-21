using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
*/

namespace Repositories.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Event>? Events { get; set; }

        public Producer()
        {

        }

    }
}
