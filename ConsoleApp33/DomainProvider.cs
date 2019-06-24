using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp33
{

    public class DomainProvider : IDomainProvider
    {
        public async Task<List<string>> Get() => (await File.ReadAllLinesAsync("Domains.txt").ConfigureAwait(false)).ToList();
    }
}
