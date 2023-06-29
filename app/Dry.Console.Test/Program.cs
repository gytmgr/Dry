#nullable enable

using Dry.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace Dry.Console.Test
{
    public class QQ
    {
        public string AA { get; set; }
        public string ZZ { get; set; }

        public WW? WW { get; set; }
    };
    public class WW
    {
        public string SS { get; set; }
        public string XX { get; set; }
    }


    class Program
    {
        static async Task Main(string[] args)
        {
            var from = new QQ { AA = "from", ZZ = "from", WW = new WW { SS = "from", XX = "from" } };
            var to = new QQ { AA = "to", ZZ = "to", WW = null };
            from.CopyProperty(to, "ZZ", "WW.XX");
            System.Console.ReadKey();
        }

        static async void GG()
        {
            System.Console.WriteLine(DateTime.Now);
            await Task.Delay(TimeSpan.FromSeconds(5));
            System.Console.WriteLine(DateTime.Now);
            await Task.Delay(TimeSpan.FromSeconds(5));
            System.Console.WriteLine(DateTime.Now);
        }
    }
}