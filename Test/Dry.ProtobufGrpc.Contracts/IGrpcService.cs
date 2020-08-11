using System.ServiceModel;
using System.Threading.Tasks;

namespace Dry.ProtobufGrpc.Contracts
{
    [ServiceContract]
    public interface IGrpcService
    {
        [OperationContract]
        Task<string> Get(string param);
    }
}