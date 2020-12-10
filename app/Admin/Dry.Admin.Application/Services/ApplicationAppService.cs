using Dry.Admin.Application.Contracts.Dtos;
using Dry.Admin.Application.Contracts.Services;
using Dry.Admin.Domain;
using Dry.Admin.Domain.ValueObjects;
using Dry.Application.Services;
using Dry.Core.Model;
using Dry.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using App = Dry.Admin.Domain.Entities.Application;

namespace Dry.Admin.Application.Services
{
    public class ApplicationAppService : ApplicationQueryService<IAdminContext, App, ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService
    {
        public ApplicationAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override IQueryable<App> GetQueryable([NotNull] IQueryable<App> queryable, ApplicationQueryDto queryDto)
        {
            queryable = base.GetQueryable(queryable, queryDto);
            if (queryDto != null)
            {
                if (queryDto.TypeId.HasValue)
                {
                    queryable = queryable.Where(x => x.Type == (ApplicationType)queryDto.TypeId.Value);
                }
                if (!string.IsNullOrEmpty(queryDto.NameLike))
                {
                    queryable = queryable.Where(x => x.Name.Contains(queryDto.NameLike));
                }
                if (queryDto.Enable.HasValue)
                {
                    queryable = queryable.Where(x => x.Enable == queryDto.Enable.Value);
                }
            }
            return queryable;
        }

        public async override Task<ApplicationDto> CreateAsync([NotNull] ApplicationCreateDto createDto)
        {
            if (await _repository.AnyAsync(x => x.Id == createDto.Id))
            {
                throw new BizException("编码已存在");
            }
            var entity = _mapper.Map<App>(createDto);
            entity.Secret = Guid.NewGuid().ToString().Replace("-", string.Empty);
            entity.AddTime = DateTime.Now;
            await _repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ApplicationDto>(entity);
        }

        public Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        {
            var data = EnumHelper.GetEnumDic<ApplicationType>().ToArray();
            return Task.FromResult(data);
        }
    }
}