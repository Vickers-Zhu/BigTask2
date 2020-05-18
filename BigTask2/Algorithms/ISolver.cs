using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigTask2.Api;
using BigTask2.Data;

namespace BigTask2.Algorithms
{
    interface ISolver
    {
        IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to);
    }
}
