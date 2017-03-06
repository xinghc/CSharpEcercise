using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWget
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new WebClient();
            string data = cli.DownloadString("https://archive.ics.uci.edu/ml/machine-learning-databases/undocumented/connectionist-bench/sonar/sonar.all-data");
            Console.WriteLine(data);
            Console.ReadKey();
        }
    }
}
