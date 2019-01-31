using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace proj_promsg_for_cpp
{
    public class ProtocolConfig
    {
        public struct Property
        {
            public string Name;
            public string DataType;
            public string Default;
        }

        public struct Parameter
        {
            public string Name;
            public List<Property> Properties;
        }

        public ProtocolConfig()
        {
            Name = String.Empty;
            ParamList = new List<Parameter>();
        }

        public void read(string filename)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filename);

                XmlElement root = document.SelectSingleNode("/protocol") as XmlElement;
                Name = root.GetAttribute("name");

                XmlNodeList param_list = document.SelectNodes("/protocol/param");
                foreach(var param in param_list)
                {
                    XmlElement parameter = param as XmlElement;
                    XmlNodeList property_list = parameter.SelectNodes("property");

                    Parameter pm;
                    pm.Name = parameter.GetAttribute("name");
                    pm.Properties = new List<Property>();

                    foreach (var proper in property_list)
                    {
                        XmlElement property = proper as XmlElement;
                        Property pp;
                        pp.Name = property.GetAttribute("name");
                        pp.DataType = property.GetAttribute("type");
                        pp.Default = property.GetAttribute("default");
                        pm.Properties.Add(pp);
                    }

                    ParamList.Add(pm);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public string Name;
        public List<Parameter> ParamList;
    }
}
