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
            res = DataTypeMap.DMap[pp.DataType] + " " + pp.Name + ";";
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
            foreach (var pp in pm.Properties)
            {
                if(res.Length > 0)
                {
                    res += ", ";
                }
                res += pp.Name;
            }
            return res;
        }

        public static string gen_ctor_param_list(ProtocolConfig.Parameter pm)
        {
            string res = String.Empty;
            foreach (var pp in pm.Properties)
            {
                if (res.Length > 0)
                {
                    res += ", ";
                }
                res += DataTypeMap.DMap[pp.DataType] + " " + pp.Name + "_";
            }
            return res;
        }

        public static string gen_copy_constructor(ProtocolConfig.Parameter pm)
        {
            string res = pm.Name + "(\n\t\t";
            for(int i = 0; i < pm.Properties.Count(); ++i)
            {
                if(i > 0)
                {
                    res += "\n\t\t, ";
                }
                else if(i == 0)
                {
                    res += "  ";
                }
                res += DataTypeMap.DMap[pm.Properties[i].DataType] + " " + pm.Properties[i].Name + "_";
            }
            res += "\n\t)\n\t\t:";

            for (int i = 0; i < pm.Properties.Count(); ++i)
            {
                if (i > 0)
                {
                    res += "\n\t\t, ";
                }
                else if (i == 0)
                {
                    res += " ";
                }

                res += pm.Properties[i].Name + "(";

                switch(pm.Properties[i].DataType)
                {
                case "string":
                    res += "\"" + pm.Properties[i].Default + "\"";
                    break;

                default:
                    res += pm.Properties[i].Default;
                    break;
                }

                res += ")";
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
