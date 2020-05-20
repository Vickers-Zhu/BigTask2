using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;

namespace BigTask2.RequestHandler
{
    class MergingDatase : IRequest
    {
        IRequest next;
        IGraphDatabase adjDb;
        IGraphDatabase matDb;

        public MergingDatase(IGraphDatabase adjDb, IGraphDatabase matDb)
        {
            this.adjDb = adjDb;
            this.matDb = matDb;
        }
        
        public IEnumerable<Route> Handle(Request request, IRouteProblem problem)
        {
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Car) &&
                request.Filter.AllowedVehicles.Contains(VehicleType.Train))
            {
                matDb.Merging(adjDb);
                adjDb.Merging(matDb);
                problem.Graph = adjDb;
                return this.next.Handle(request, problem);
            }
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Car))
            {
                problem.Graph = adjDb;
                return this.next.Handle(request, problem);
            }
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Train))
            {
                problem.Graph = matDb;
                return this.next.Handle(request, problem);
            }
            Console.WriteLine("Merging failed");
            return null;           
        }

        public void SetNext(IRequest request)
        {
            this.next = request;
        }
    }
}
