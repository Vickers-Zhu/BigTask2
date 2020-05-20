//This file Can be modified

using BigTask2.Data;
using BigTask2.Algorithms;
using System.Collections.Generic;
using BigTask2.Api;

namespace BigTask2.Interfaces
{
    interface IRouteProblem
    {
        IGraphDatabase Graph { get; set; }
        IEnumerable<Route> Results { get; set; }
        void Accept(ISolver solver);
	}
}
