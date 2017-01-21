using Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Server.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            int errCode = 0;
            if (SvrManager.StartAllService(out errCode))
            {
                Console.WriteLine("服务启动成功！");
            }
            else
            {
                Console.WriteLine(errCode);
            }
            Console.ReadLine();
        }
    }
}
