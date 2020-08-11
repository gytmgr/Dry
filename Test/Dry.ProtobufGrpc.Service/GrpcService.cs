using Dry.ProtobufGrpc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dry.ProtobufGrpc.Service
{
    public class GrpcService : IGrpcService
    {
        public Task<string> Get(string param)
        {
            return Task.FromResult(param);
        }
    }
}