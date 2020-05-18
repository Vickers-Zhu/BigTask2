//This file Can be modified

using BigTask2.Data;
using BigTask2.Interfaces;

namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
		public string From, To;
		public CostProblem(string from, string to)
		{
			From = from;
			To = to;
		}

        public IGraphDatabase Graph { get; set; }
	}
}
