using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishingYear { get; set; }
        public string Publisher { get; set; }
        public int copiesInStock { get; set; }
        public int copiesBorrowed { get; set; }
    }
}
