using System.Configuration;

namespace RallyCat.Core.Configuration
{
    public class RallyConfigurationSection : ConfigurationSection
    {
        //[ConfigurationProperty("projects")]
        //public RallyProjectCollection Projects
        //{
        //    get { return ((RallyProjectCollection)(base["projects"])); }
        //}


        [ConfigurationProperty("rallyUrl")]
        public string RallyUrl
        {
            get { return (string)this["rallyUrl"]; }
            set { this["rallyUrl"] = value; }
        }

        //[ConfigurationProperty("enableGoogleResponse")]
        //public bool EnableGoogleResponse
        //{
        //    get { return (string)this["rallyUrl"]; }
        //    set { this["rallyUrl"] = value; }
        //}

        

    }

    public class RallyProjectElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }


        [ConfigurationProperty("name", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string UserName
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Password
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }
    }
}
