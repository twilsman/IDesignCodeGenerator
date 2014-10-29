using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public class DalGeneratorConfigSection : ConfigurationSection
    {
        public static readonly DalGeneratorConfigSection current = (DalGeneratorConfigSection)ConfigurationManager.GetSection("dalGenerator");

        [ConfigurationProperty("BaseNamespace", DefaultValue = "")]
        public string BaseNamespace
        {
            get { return (string)base["BaseNamespace"]; }
            set { base["BaseNamespace"] = value; }
        }

        [ConfigurationProperty("TargetDbConnectionStringName", DefaultValue = "")]
        public string TargetDbConnectionStringName
        {
            get { return (string)base["TargetDbConnectionStringName"]; }
            set { base["TargetDbConnectionStringName"] = value; }
        }

        [ConfigurationProperty("DataContractsPath", DefaultValue = "")]
        public string DataContractsPath
        {
            get { return (string)base["DataContractsPath"]; }
            set { base["DataContractsPath"] = value; }
        }

        [ConfigurationProperty("AccessorsPath", DefaultValue = "")]
        public string AccessorsPath
        {
            get { return (string)base["AccessorsPath"]; }
            set { base["AccessorsPath"] = value; }
        }

        [ConfigurationProperty("ContractsPath", DefaultValue = "")]
        public string ContractsPath
        {
            get { return (string)base["ContractsPath"]; }
            set { base["ContractsPath"] = value; }
        }

        [ConfigurationProperty("UnitTestsPath", DefaultValue = "")]
        public string UnitTestsPath
        {
            get { return (string)base["UnitTestsPath"]; }
            set { base["UnitTestsPath"] = value; }
        }

        [ConfigurationProperty("DbContextClassName", DefaultValue = "")]
        public string DbContextClassName
        {
            get { return (string)base["DbContextClassName"]; }
            set { base["DbContextClassName"] = value; }
        }

        [ConfigurationProperty("BaseAccessorClassName", DefaultValue = "")]
        public string BaseAccessorClassName
        {
            get { return (string)base["BaseAccessorClassName"]; }
            set { base["BaseAccessorClassName"] = value; }
        }

        [ConfigurationProperty("CreateBaseAccessorClass", DefaultValue = false)]
        public bool CreateBaseAccessorClass
        {
            get { return (bool)base["CreateBaseAccessorClass"]; }
            set { base["CreateBaseAccessorClass"] = value; }
        }

        [ConfigurationProperty("TemplatesPath", DefaultValue = @"..\..\..\templates")]
        public string TemplatesPath
        {
            get { return (string)base["TemplatesPath"]; }
            set { base["TemplatesPath"] = value; }
        }

        [ConfigurationProperty("includedTables", IsDefaultCollection = false)]
        public IncludedTablesCollection IncludedTables
        {
            get
            {
                IncludedTablesCollection includedTables = (IncludedTablesCollection)base["includedTables"];
                return includedTables;
            }
        }

    }

    public class IncludedTablesCollection : ConfigurationElementCollection
    {
        public IncludedTablesElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as IncludedTablesElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new IncludedTablesElement();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((IncludedTablesElement)element).Name;
        }
    }

    public class IncludedTablesElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("schema", IsRequired = false)]
        public string Schema
        {
            get
            {
                return this["schema"] as string;
            }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }
    }

}
