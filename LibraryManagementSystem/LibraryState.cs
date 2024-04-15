using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class LibraryState
    {
        public int TotalItems { get; set; }
        public int AvailableItems { get; set; }
        public int BorrowedItems { get; set; }
        public int ActiveBorrowers { get; set; }
        public int OverdueItems { get; set; }
    }
}
