#nullable enable

using Dry.Core.Model;
using Dry.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Dry.Console.Test
{
    public interface IQQ
    {
        void SetAA(string aa);

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
            var requester = new HttpRequester(HttpMethod.Get, "http://localhost:61073/api/Application");
            requester.Headers = new Collection<KeyValuePair<string, string>>();
            requester.Headers.Add(new KeyValuePair<string, string>("TenantId", "Test"));
            var result = await requester.GetResultAsync<JsonNode[]>();
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