using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp33
{
    public interface IDomainProvider
    {
        Task<List<string>> Get();
    }
}
