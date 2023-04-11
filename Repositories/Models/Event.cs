using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public virtual ICollection<Producer>? Producers { get; set; }

        public Event()
        {

        }
    }
}
