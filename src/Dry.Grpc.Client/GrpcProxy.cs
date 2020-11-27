using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;

namespace Dry.Grpc.Client
{
    /// <summary>
    /// grpc代理
    /// </summary>
    public class GrpcProxy : IDisposable
    {
        /// <summary>
        /// grpc通讯通道
        /// </summary>
        private readonly GrpcChannel _grpcChannel;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcProxy()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="address"></param>
        private GrpcProxy(string address)
        {
            _grpcChannel = GrpcChannel.ForAddress(address);
        }

        /// <summary>
        /// 获取grpc代理
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static GrpcProxy GetInstance(string address)
        {
            return new GrpcProxy(address);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetService<TService>() where TService : class
        {
            return _grpcChannel.CreateGrpcService<TService>();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _grpcChannel?.Dispose();
        }
    }
}