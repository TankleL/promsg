using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_promsg_for_cpp
{
    public class ParamGenerator
    {
        private static string template_path = @"../../../../templates/for-cpp/param.hpp";

        public static string gen_property(ProtocolConfig.Property pp)
        {
            string res = String.Empty;
            res = DataTypeMap.Convert(pp.DataType, pp.DataType) + " " + pp.Name + ";";
            return res;
        }

        public static string gen_properties(ProtocolConfig.Parameter pm)
        {
            string res = String.Empty;
            foreach(var pp in pm.Properties)
            {
                res += gen_property(pp) + "\n\t";
            }
            return res;
        }

        public static string gen_property_list(ProtocolConfig.Parameter pm)
        {
            string res = String.Empty;
            for(int i = 0; i < pm.Properties.Count(); ++i)
            {
                var pp = pm.Properties[i];

                if(i > 0)
                {
                    res += ",\n\t\t";
                }
                else if(i == 0)
                {
                    res += "\n\t\t";
                }

                res += pp.Name;
            }
            return res;
        }

        public static string gen_copy_constructor(ProtocolConfig.Parameter pm)
        {
            string res = pm.Name + "(\n\t\t";
            for(int i = 0; i < pm.Properties.Count(); ++i)
            {
                ProtocolConfig.Property pp = pm.Properties[i];

                if (i > 0)
                {
                    res += "\n\t\t, ";
                }
                else if(i == 0)
                {
                    res += "  ";
                }
                res += DataTypeMap.Convert(pp.DataType, pp.DataType) + " " + pp.Name + "_";
            }
            res += "\n\t)\n\t\t:";

            for (int i = 0; i < pm.Properties.Count(); ++i)
            {
                ProtocolConfig.Property pp = pm.Properties[i];
                if (i > 0)
                {
                    res += "\n\t\t, ";
                }
                else if (i == 0)
                {
                    res += " ";
                }

                res += pp.Name + "(" + pp.Name + "_)";
            }

            res += "\n\t{}";

            return res;
        }

        public static string generate(ProtocolConfig.Parameter pm)
        {
            string target = System.IO.File.ReadAllText(template_path);
            target = target.Replace("$(PARAM_NAME)", pm.Name);
            target = target.Replace("$(PROPERTIES)", gen_properties(pm));
            target = target.Replace("$(PROPERTIES_LIST)", gen_property_list(pm));
            target = target.Replace("$(CONSTRUCTOR_COPY)", gen_copy_constructor(pm));
            return target;
        }
    }
}
