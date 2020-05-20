using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Interfaces;
using BigTask2.Algorithms;

namespace BigTask2.RequestHandler
{
    class PickSolver : IRequest
    {
        IRequest next;
        public IEnumerable<Route> Handle(Request request, IRouteProblem problem)
        {
            ISolver solver = null;
            if (request.Solver == "BFS")
            {
                solver = new BFS();
            }
            if (request.Solver == "DFS")
            {
                solver = new DFS();
            }
            if (request.Solver == "Dijkstra")
            {
                if (request.Problem == "Time")
                    solver = new DijkstraTime();
                if (request.Problem == "Cost")
                    solver = new DijkstraCost();
            }
            if (solver != null)
            {
                problem.Accept(solver);
                return problem.Results;
            }
            Console.WriteLine("Pick Solver failed");
            return null;
        }

        public void SetNext(IRequest request)
        {
            this.next = request;
        }
    }
}
