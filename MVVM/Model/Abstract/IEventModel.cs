using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.Abstract
{
    public interface IEventModel
    {
        int Id { get; set; }
        int StateId { get; set; }
        int UserId { get; set; }
        DateTime DateStamp { get; set; }
        string EventType { get; set; }
    }
}
