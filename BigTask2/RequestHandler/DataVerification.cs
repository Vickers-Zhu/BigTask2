using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Interfaces;

namespace BigTask2.RequestHandler
{
    class DataVerification : IRequest
    {
        IRequest next;
        public IEnumerable<Route> Handle(Request request, IRouteProblem problem)
        {
            if (Test(request)) return next.Handle(request, problem);
            Console.WriteLine("Verify failed");
            return null;
        }

        private bool Test(Request request)
        {
            if (request.From != null &&
                request.To != null &&
                request.Filter.MinPopulation >= 0 &&
                request.Filter.AllowedVehicles.Count > 0)
                return true;
            return false;
        }

        public void SetNext(IRequest request)
        {
            this.next = request;
        }
    }
}
