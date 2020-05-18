//This file Can be modified

using BigTask2.Data;
using BigTask2.Interfaces;

namespace BigTask2.Problems
{
	class TimeProblem : IRouteProblem
	{
        public IGraphDatabase Graph { get; set; }
        public string From, To;
		public TimeProblem(string from, string to)
		{
			From = from;
			To = to;
		}
	}
}
