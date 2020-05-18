//This file contains fragments that You have to fulfill

using BigTask2.Api;
using BigTask2.Data;
using System.Collections.Generic;

namespace BigTask2.Algorithms
{
	public class DFS : ISolver
    {
		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, Route> routes = new Dictionary<City, Route>();
			routes[from] = null;
			Stack<City> stack = new Stack<City>();
			stack.Push(from);
			do
			{
				City city = stack.Pop();
                foreach (Route rt in graph.GetRoutesFrom(city))
                {
                    Route route = rt; /* Change to current Route*/
					if (routes.ContainsKey(route.To))
					{
						continue;
					}
					routes[route.To] = route;
					if (route.To == to)
					{
						break;
					}
					stack.Push(route.To);
				}
			} while (stack.Count > 0);
			if (!routes.ContainsKey(to))
			{
				return null;
			}
			List<Route> result = new List<Route>();
			for (Route route = routes[to]; route != null; route = routes[route.From])
			{
				result.Add(route);
			}
			result.Reverse();
			return result;
		}
	}
}
