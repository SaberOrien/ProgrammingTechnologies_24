using Logic.DTOs_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs_Implemented
{
    internal class StateDTO : IStateDTO
    {
        public StateDTO(int id, int itemId, int itemAmount)
        {
            this.Id = id;
            this.ItemId = itemId;
            this.ItemAmount = itemAmount;
        }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
    }
}
