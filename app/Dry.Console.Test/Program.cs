#nullable enable

using Dry.Console.Test.Demo;
using Dry.Core.Model;
using Dry.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Dry.Console.Test
{
    public interface IQQ : ISingletonDependency<IQQ>
    {
        void SetAA(string aa);

        static string? Key { get; }
    }

    public abstract class QQ<T> : IQQ where T : class, IQQ
    {
        public static string AA { get; set; }
        public QQ()
        {
            Instance = this as T;
        }

        public void SetAA(string aa)
        {
            AA = aa;
        }

        public static T Instance { get; set; }
    };

    public class RR : IQQ, ISingletonDependency<IQQ>
    {
        public static object? ServiceKey { get; } = "gg";

        public void SetAA(string aa)
        {
            throw new NotImplementedException();
        }

        public static string? Key => "gg";
    }

    public class WW : QQ<WW>
    {
        public int BB { get; set; }
    }
    public class EE : QQ<EE>, ISingletonDependency<EE>
    {
        public EE()
        {
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.ReadKey();
        }

        static async Task GG(int no, WW ww)
        {
            ww.BB++;
            System.Console.WriteLine($"GG{no} Start: {DateTime.Now}");
            await Task.Delay(5000);
            System.Console.WriteLine($"GG{no} End: {DateTime.Now}");
        }
    }
}