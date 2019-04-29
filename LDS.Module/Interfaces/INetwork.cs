using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS.Module.Interfaces
{
    public interface INetwork
    {
        string Name { get; set; }
        string NetworkLeader { get; set; }
    }
}
