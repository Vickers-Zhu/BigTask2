//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data
{
	class AdjacencyListDatabase : IGraphDatabase
    {
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private Dictionary<City, List<Route>> routes = new Dictionary<City, List<Route>>();
        private City from;
        private City to;
        private Filter filter;
		
		private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name)) 
				cityDictionary[city.Name] = city;
		}
		public AdjacencyListDatabase(IEnumerable<Route> routes)
		{
			foreach(Route route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
				if (!this.routes.ContainsKey(route.From))
				{
					this.routes[route.From] = new List<Route>();
				}
				this.routes[route.From].Add(route);
			}
		}
		public AdjacencyListDatabase()
		{
		}
		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			if (!routes.ContainsKey(from))
			{
				routes[from] = new List<Route>();
			}
			routes[from].Add(new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle});
		}

        public void Merging(IGraphDatabase another)
        {
            foreach (var item in cityDictionary)
            {
                City city = another.GetByName(item.Key);
                if (city != null)
                {
                    foreach (Route route in another.GetRoutesFrom(city))
                    {
                        if (!routes[city].Contains(route))
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
            foreach (Route route in routes[from])
            {
                if(RouteFilter(route))
                    if (!results.Contains(route)) results.Add(route);
            }
            return results;
		}

		public City GetByName(string cityName)
		{
            return cityDictionary.GetValueOrDefault(cityName);
        }

        private bool RouteFilter(Route route)
        {
            if (filter == null) return true;
            if (route.From == from)
            {
                if(route.To != to)
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
