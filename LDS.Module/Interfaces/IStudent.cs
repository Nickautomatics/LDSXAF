using LDS.Module.PublicEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Interfaces
{
    public interface IStudent:IPeople
    {
        Program Program { get; set; }
    }
}
