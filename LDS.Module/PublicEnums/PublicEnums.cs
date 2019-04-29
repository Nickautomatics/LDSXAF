using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.PublicEnums
{
    public enum Program
    {
        None = 0,
        LifeClass = 1,
        SOL1 = 2,
        SOL2 = 3,
        SOL3 = 4,
        Administrators = 5
    }

    public enum AttendanceMode
    {
        None = 0,
        Present = 1,
        Absent = 2,
        Excused = 3,
        MakedUp = 4
    }
}
