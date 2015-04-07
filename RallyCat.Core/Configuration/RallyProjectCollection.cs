using System.Configuration;

namespace RallyCat.Core.Configuration
{
    public class RallyProjectCollection : ConfigurationElementCollection
    {
        public ConfigurationElement this[int index]
        {
            get { return (ConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RallyProjectElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RallyProjectElement)(element)).Name;
        }
    }
}