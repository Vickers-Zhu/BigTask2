//This file Can be modified

using System.Collections.Generic;
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;

namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
        public IGraphDatabase Graph { get; set; }
        public IEnumerable<Route> Results { get; set; }

        public string From, To;
        public CostProblem(string from, string to)
		{
			From = from;
			To = to;
		}

        public void Accept(ISolver solver)
        {
            Results = solver.Solve(this.Graph, Graph.GetByName(From), Graph.GetByName(To));
        }
    }
}
