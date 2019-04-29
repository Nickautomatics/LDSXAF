using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Interfaces
{
    public interface ITeacher:IPeople
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}
