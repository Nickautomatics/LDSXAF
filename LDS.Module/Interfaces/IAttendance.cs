using LDS.Module.PublicEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Interfaces
{
    public interface IAttendance
    {
        AttendanceMode Week1 { get; set; }
        AttendanceMode Week2 { get; set; }
        AttendanceMode Week3 { get; set; }
        AttendanceMode Week4 { get; set; }
        AttendanceMode Week5 { get; set; }
        AttendanceMode Week6 { get; set; }
        AttendanceMode Week7 { get; set; }
        AttendanceMode Week8 { get; set; }
        AttendanceMode Week9 { get; set; }
        AttendanceMode Week10 { get; set; }
    }
}
