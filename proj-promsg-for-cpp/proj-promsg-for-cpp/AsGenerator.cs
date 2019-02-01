using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_promsg_for_cpp
{
    public class AsGenerator
    {
        private static string template_path = @"../../../../templates/for-cpp/as.hpp";

        public static string gen_construct_param(ProtocolConfig.Parameter pm)
        {
            string res = String.Empty;

            for(int i = 0; i < pm.Properties.Count(); ++i)
            {
                ProtocolConfig.Property pp = pm.Properties[i];
                res += "\n\t\t\t\t\t\t";

                string val = String.Empty;
                switch(pp.DataType)
                {
                case "string":
                    val = "\"" + pp.Default + "\"";
                    break;

                default:
                    val = pp.Default;
                    break;
                }

                if(i > 0)
                {
                    res += ", " + val;
                }
                else if(i == 0)
                {
                    res += "  " + val;
                }
            }

            return res;
        }

        public static string gen_decode_val(ProtocolConfig.Parameter pm)
        {
            string res = String.Empty;

            for(int i = 0; i < pm.Properties.Count(); ++i)
            {
                ProtocolConfig.Property pp = pm.Properties[i];
                res += "// decode " + pp.Name + "\n\t\t\t\t\t";
                res += "if( o.via.array.size > " + Convert.ToString(i) + " ){\n\t\t\t\t\t";
                res += "\tobj." + pp.Name + " = " + "o.via.array.ptr[" + Convert.ToString(i) + "].as<" +
                    DataTypeMap.Convert(pp.DataType, pp.DataType) + ">();\n\t\t\t\t\t}\n\t\t\t\t\t";
            }

            return res;
        }

        public static string generate(ProtocolConfig.Parameter pm)
        {
            string target = System.IO.File.ReadAllText(template_path);

            target = target.Replace("$(PARAM_NAME)", pm.Name);
            target = target.Replace("$(CONSTRUCT_PARAM)", gen_construct_param(pm));
            target = target.Replace("$(DECODE_VALUE)", gen_decode_val(pm));

            return target;
        }
    }
}
