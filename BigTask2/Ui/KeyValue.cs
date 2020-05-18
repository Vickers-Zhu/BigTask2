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

        public void Accept(ISolver solver)
        {
            throw new NotImplementedException();
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
            return elements[1].Trim();
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
            throw new NotImplementedException();
        }
    }
}
