using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigTask2.Algorithms;
using BigTask2.Api;

namespace BigTask2.Ui
{
    class KeyValue : ISystem
    {
        public IForm Form { get; }

        public IDisplay Display { get; }

        public KeyValue(IForm form, IDisplay display)
        {
            this.Form = form;
            this.Display = display;
        }
    }

    class KeyForm : IForm
    {
        private readonly string pattern = @"\=";
        private Dictionary<string, string> data = new Dictionary<string, string>();
        public bool GetBoolValue(string name)
        {
            return bool.Parse(data[name]);
        }

        public int GetNumericValue(string name)
        {
            return int.Parse(data[name]);
        }

        public string GetTextValue(string name)
        {
            return data[name];
        }

        public void Insert(string command)
        {
            data[KeyKey(command)] = ValueKey(command);
        }

        private string ValueKey(string name)
        {
            //Checked
            string[] elements = System.Text.RegularExpressions.Regex.Split(name, pattern);
            if (elements.Count() > 1)
                return elements[1].Trim();
            return null;
        }

        private string KeyKey(string name)
        {
            //Checked
            string[] elements = System.Text.RegularExpressions.Regex.Split(name, pattern);
            return elements[0].Trim();
        }
    }

    class KeyDisplay : IDisplay
    {
        public void Print(IEnumerable<Route> routes)
        {
            if (routes == null) return;
            KvDec decorator = new KvDec();
            City city = null;
            foreach (Route route in routes)
            {
                if (city != route.From)
                {
                    city = route.From;
                    Console.WriteLine("=City=");
                    decorator.Set("Name", city.Name);
                    Console.WriteLine(decorator.Result());
                    decorator.Set("Population", city.Population.ToString());
                    Console.WriteLine(decorator.Result());
                    decorator.Set("HasRestaurant", city.HasRestaurant.ToString());
                }
                Console.WriteLine();
                Console.WriteLine("=Route=");
                decorator.Set("Vehicle", route.VehicleType.ToString());
                Console.WriteLine(decorator.Result());
                decorator.Set("Cost", route.Cost.ToString());
                Console.WriteLine(decorator.Result());
                decorator.Set("TravelTime", route.TravelTime.ToString());
                Console.WriteLine(decorator.Result());
                Console.WriteLine();
                if (city != route.To)
                {
                    city = route.To;
                    Console.WriteLine("=City=");
                    decorator.Set("Name", city.Name);
                    Console.WriteLine(decorator.Result());
                    decorator.Set("Population", city.Population.ToString());
                    Console.WriteLine(decorator.Result());
                    decorator.Set("HasRestaurant", city.HasRestaurant.ToString());
                }
            }
            Console.WriteLine();
            decorator.Set("totalTime", Util.TimeSum(routes).ToString());
            Console.WriteLine(decorator.Result());
            decorator.Set("totalCost", Util.CostSum(routes).ToString());
            Console.WriteLine(decorator.Result());
            Console.WriteLine();
        }
    }
}
