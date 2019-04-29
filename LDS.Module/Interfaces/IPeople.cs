using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Interfaces
{
    public interface IPeople
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Nickname { get; set; }
        string CellLeader { get; set; }
        string ContactOfCellLeader { get; set; }
        DateTime Birthday { get; set; }

    }
}
