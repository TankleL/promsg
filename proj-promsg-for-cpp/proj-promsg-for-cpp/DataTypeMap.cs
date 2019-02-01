using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proj_promsg_for_cpp
{
    public class DataTypeMap
    {
        public static Dictionary<string, string> DMap;

        public static void init()
        {
            try
            {
                StreamReader sr = new StreamReader(@"../../../../templates/for-cpp/dtmap.cfg");
                string line = String.Empty;

                DMap = new Dictionary<string, string>();
                while((line = sr.ReadLine()) != null)
                {
                    string[] separators = { "=>" };
                    string[] item = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    if(item.Length == 2)
                    {
                        DMap.Add(item[0].Trim(), item[1].Trim());
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static string Convert(string origin, string default_type)
        {
            try
            {
                string res = DMap[origin];
                return res;
            }
            catch(Exception)
            {
                return default_type;
            }
        }
    }
}
