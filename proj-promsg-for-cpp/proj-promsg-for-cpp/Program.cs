using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_promsg_for_cpp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("wrong arguments.");
                return;
            }

            DataTypeMap.init();

            foreach(string filename in args)
            {
                ProtocolConfig pc = new ProtocolConfig();
                pc.read(filename);

                foreach(var param in pc.ParamList)
                {
                    string gen = ParamGenerator.generate(param);
                }
            }
        }
    }
}
