using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class Event
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public DateTime Timestamp { get; set; }
        public EventType Type { get; set; }
    }
}
