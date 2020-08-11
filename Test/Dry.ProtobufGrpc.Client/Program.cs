using Dry.Grpc.Client;
using Dry.ProtobufGrpc.Contracts;
using System;

namespace Dry.ProtobufGrpc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using var proxy = GrpcProxy.GetInstance("http://localhost:7777");
            var service = proxy.GetService<IGrpcService>();
            var result = service.Get("fdasfa").GetAwaiter().GetResult();

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
