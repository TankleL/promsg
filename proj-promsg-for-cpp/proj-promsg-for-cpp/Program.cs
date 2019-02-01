using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proj_promsg_for_cpp
{
    class Program
    {
        static void Main(string[] args)
        {
            string output_path = String.Empty;
            DataTypeMap.init();

            Dictionary<string, string> generated = new Dictionary<string, string>();
            foreach (string cmd in args)
            {
                if(cmd.IndexOf("-o") == 0)
                {
                    output_path = cmd.Substring(2);
                }
                else
                {
                    ProtocolConfig pc = new ProtocolConfig();
                    pc.read(cmd);
                    generated.Add(cmd, ProtocolGenerator.generate(pc));
                }
            }

            foreach(var code in generated)
            {
                string path = Path.GetDirectoryName(code.Key);
                string name = Path.GetFileName(code.Key);

                path = output_path.Length > 0 ? output_path : path;

                int pos_del = name.LastIndexOf(".xml");
                if(pos_del >= 0 && name.Length - ".xml".Length == pos_del)
                {
                    name = name.Substring(0, pos_del);
                }

                name = path + "\\" + name + ".hpp";
                File.WriteAllText(name, code.Value);
            }
        }
    }
}
