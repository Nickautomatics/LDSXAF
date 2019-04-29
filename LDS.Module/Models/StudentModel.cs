using LDS.Module.Interfaces;
using LDS.Module.PublicEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Models
{
    public class StudentModel : IStudent , IAttendance
    {
        public int Batch { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Program Program { get; set; }
        public string Nickname { get; set; }
        public string CellLeader { get; set; }
        public string ContactOfCellLeader { get; set; }
        public AttendanceMode Week1 { get; set; }
        public AttendanceMode Week2 { get; set; }
        public AttendanceMode Week3 { get; set; }
        public AttendanceMode Week4 { get; set; }
        public AttendanceMode Week5 { get; set; }
        public AttendanceMode Week6 { get; set; }
        public AttendanceMode Week7 { get; set; }
        public AttendanceMode Week8 { get; set; }
        public AttendanceMode Week9 { get; set; }
        public AttendanceMode Week10 { get; set; }
    }
}
