using Dry.Admin.Application.Contracts.Dtos;
using Dry.Admin.Application.Contracts.Services;
using Dry.Application.RESTFul.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dry.Admin.Application.RESTFul.Api.Controllers
{
    /// <summary>
    /// 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ApplicationController : ApplicationQueryController<IApplicationAppService, ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService
    {
        /// <summary>
        /// 获取应用类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("Type")]
        public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        {
            return await AppService.TypeArrayAsync();
        }
    }
}