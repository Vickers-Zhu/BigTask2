//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;
using System.Linq;

namespace BigTask2.Data
{
	class MatrixDatabase : IGraphDatabase
	{
		private Dictionary<City, int> cityIds = new Dictionary<City, int>();
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private List<List<Route>> routes = new List<List<Route>>();
        private City from;
        private City to;
        private Filter filter;

        private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
			{
				cityDictionary[city.Name] = city;
				cityIds[city] = cityIds.Count;
				foreach (var routes in routes)
				{
					routes.Add(null);
				}
				routes.Add(new List<Route>(Enumerable.Repeat<Route>(null, cityDictionary.Count)));
			}
		}
		public MatrixDatabase(IEnumerable<Route> routes)
		{
            foreach (var route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
			}
			foreach (var route in routes)
			{
				this.routes[cityIds[route.From]][cityIds[route.To]] = route;
			}
		}

		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			routes[cityIds[from]][cityIds[to]] = new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle };
		}

        public void Merging(IGraphDatabase another)
        {
            foreach (var item in cityDictionary)
            {
                City city = another.GetByName(item.Key);
                if (city != null)
                {
                    foreach (Route route in routes[cityIds[city]])
                    {
                        if (!routes[cityIds[city]].Contains(route))
                            AddRoute(route.From, route.To, route.Cost, route.TravelTime, route.VehicleType);
                    }
                }
            }        
        }

        public void Filtering(int MinPopulation, bool RestaurantRequired,
            ISet<VehicleType> AllowedVehicles, City from, City to)
        {
            this.from = from;
            this.to = to;
            this.filter = new Filter
            {
                MinPopulation = MinPopulation,
                RestaurantRequired = RestaurantRequired,
                AllowedVehicles = AllowedVehicles
            };
        }

        public IEnumerable<Route> GetRoutesFrom(City from)
		{
            /*
			* Fill this fragment and return Type.
			* Modyfing existing code in this class is forbidden.
			* Adding new elements (fields, private classes) to this class is allowed.
			*/
            List<Route> results = new List<Route>();
            for (int i  = 0; i < routes[cityIds[from]].Count; i++)
            {
                if (routes[cityIds[from]][i] != null)
                    if (RouteFilter(routes[cityIds[from]][i]))
                        if (!results.Contains(routes[cityIds[from]][i]))
                            results.Add(routes[cityIds[from]][i]);
            }
            return results;
		}

		public City GetByName(string cityName)
		{
			return cityDictionary[cityName];
		}

        private bool RouteFilter(Route route)
        {
            if (filter == null) return true;
            if (route.From == from)
            {
                if (route.To != to)
                    return route.To.Population >= filter.MinPopulation &&
                        route.To.HasRestaurant == filter.RestaurantRequired &&
                        filter.AllowedVehicles.Contains(route.VehicleType);
                else return filter.AllowedVehicles.Contains(route.VehicleType);

            }
            if (route.To == to)
            {
                if (route.From != from)
                    return route.From.Population >= filter.MinPopulation &&
                    route.From.HasRestaurant == filter.RestaurantRequired &&
                    filter.AllowedVehicles.Contains(route.VehicleType);
                else return filter.AllowedVehicles.Contains(route.VehicleType);
            }
            return route.From.Population >= filter.MinPopulation &&
                route.From.HasRestaurant == filter.RestaurantRequired &&
                route.To.Population >= filter.MinPopulation &&
                route.To.HasRestaurant == filter.RestaurantRequired &&
                filter.AllowedVehicles.Contains(route.VehicleType);
        }
    }
}
