﻿using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 基础查、增、改应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationCreateEditController<TService, TResult, TCreate, TEdit, TKey> :
        ApplicationCreateController<TService, TResult, TCreate, TKey>,
        IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
        where TService : IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
            => await AppService.EditAsync(id, editDto);
    }

    /// <summary>
    /// 条件查、增、改应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryCreateEditController<TService, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryCreateController<TService, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
        where TService : IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
            => await AppService.EditAsync(id, editDto);
    }
}