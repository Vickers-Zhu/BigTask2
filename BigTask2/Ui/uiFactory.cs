using System;
using System.Collections.Generic;
using System.Text;

namespace BigTask2.Ui
{
    abstract class uiFactory
    {
        protected abstract IForm GetForm();
        protected abstract IDisplay Display();
        public abstract ISystem GetSystem();
    }

    class XmlFactory : uiFactory
    {
        protected override IForm GetForm()
        {
            return new XmlForm();
        }

        protected override IDisplay Display()
        {
            return new XmlDisplay();
        }

        public override ISystem GetSystem()
        {
            return new Xml(GetForm(), Display());
        }
    }

    class KvFactory : uiFactory
    {
        protected override IForm GetForm()
        {
            return new KeyForm();
        }

        protected override IDisplay Display()
        {
            return new KeyDisplay();
        }

        public override ISystem GetSystem()
        {
            return new KeyValue(GetForm(), Display());
        }
    }
}
