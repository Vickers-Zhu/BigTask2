using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Interfaces;
using BigTask2.Problems;

namespace BigTask2.RequestHandler
{
    class PickProblem : IRequest
    {
        IRequest next;
        public IEnumerable<Route> Handle(Request request, IRouteProblem problem)
        {
            if (request.Problem == "Cost")
            {
                return this.next.Handle(request, new CostProblem(request.From, request.To));
            }
            if (request.Problem == "Time")
            {
                return this.next.Handle(request, new TimeProblem(request.From, request.To));
            }        
            Console.WriteLine("PickProblem failed");
            return null;           
        }

        public void SetNext(IRequest request)
        {
            this.next = request;
        }
    }
}
