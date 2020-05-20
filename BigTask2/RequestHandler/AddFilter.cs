using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Interfaces;

namespace BigTask2.RequestHandler
{
    class AddFilter : IRequest
    {
        IRequest next;
        public IEnumerable<Route> Handle(Request request, IRouteProblem problem)
        {
            problem.Graph.Filtering(request.Filter.MinPopulation, request.Filter.RestaurantRequired, 
                request.Filter.AllowedVehicles, problem.Graph.GetByName(request.From), 
                problem.Graph.GetByName(request.To));
            return this.next.Handle(request, problem);
        }

        public void SetNext(IRequest request)
        {
            this.next = request;
        }
    }
}
