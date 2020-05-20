using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;

namespace BigTask2.Ui
{
    abstract class Decorator
    {
        protected string key = "";
        protected string value = "";

        public Decorator()
        {
        }

        public void Set(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    class XmlDec : Decorator
    {
        public string Result()
        {
            return $"<{key}>{value}</{key}>";
        }
    }
    class KvDec : Decorator
    {
        public string Result()
        {
            return $"{key}={value}";
        }
    }
}
