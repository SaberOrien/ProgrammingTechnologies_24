using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class BorrowRecord
    {
        public int BookId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
        public decimal PenaltyAmount { get; set; } = 0m;  // Penalty for late return
        public bool IsPenaltyPaid { get; set; } = true;
    }
}
