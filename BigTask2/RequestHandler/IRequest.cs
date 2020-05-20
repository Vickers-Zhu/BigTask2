using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Interfaces;

namespace BigTask2.RequestHandler
{
    interface IRequest
    {
        IEnumerable<Route> Handle(Request request, IRouteProblem problem);
        void SetNext(IRequest request);
    }
}
