using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs_Abstract
{
    public interface IUserDTO
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Surname { get; set; }
        string UserType { get; set; }
    }
}
