﻿using Dry.Admin.Application.Contracts.Dtos;
using Dry.Admin.Application.Contracts.Services;
using Dry.Application.RESTFul.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Admin.Application.RESTFul.Client.Clients
{
    public class ApplicationClient : ApplicationQueryClient<ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService
    {
        protected override string ApiUrl => $"{AdminClientStatic.ApiUrl}/Api/Application";

        /// <summary>
        /// 获取应用类型
        /// </summary>
        /// <returns></returns>
        public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        {
            return await RequestAsync<KeyValuePair<int, string>[]>(HttpMethod.Get, "/Type");
        }
    }
}