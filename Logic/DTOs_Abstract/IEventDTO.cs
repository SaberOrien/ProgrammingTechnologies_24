using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs_Abstract
{
    public interface IEventDTO
    {
        int Id { get; set; }
        int StateId { get; set; }
        int UserId { get; set; }
        DateTime DateStamp { get; set; }
        string EventType { get; set; }
    }
}
