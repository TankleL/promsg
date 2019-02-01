using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_promsg_for_cpp
{
    public class ProtocolGenerator
    {
        private static string template_path = @"../../../../templates/for-cpp/protocol.hpp";

        public static string generate(ProtocolConfig pc)
        {
            string target = System.IO.File.ReadAllText(template_path);

            // header guard
            target = target.Replace("$(PROTOCOL_NAME)", pc.Name.ToUpper() + "PROTOCOL_HEADER");

            // gen_headers
            target = target.Replace("$(PROTOCOL_REF_HEADERS)", "");
            
            // gen_params
            string param_list = String.Empty;
            foreach (var param in pc.ParamList)
            {
                param_list += ParamGenerator.generate(param);
            }
            target = target.Replace("$(PARAM_LIST)", param_list);

            // gen_as_method
            string as_methods = String.Empty;
            foreach (var param in pc.ParamList)
            {
                as_methods += AsGenerator.generate(param);
            }
            target = target.Replace("$(AS_METHODS)", as_methods);

            return target;
        }
    }
}
