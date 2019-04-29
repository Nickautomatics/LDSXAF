using LDS.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Models
{
    public class TeacherModel : ITeacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Nickname { get; set; }
        public string CellLeader { get; set; }
        public string ContactOfCellLeader { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
