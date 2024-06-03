using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AbstractInterfaces
{
    public interface IState
    {
        int Id { get; set; }
        int ItemId { get; set; }
        int ItemAmount { get; set; }
    }
}
