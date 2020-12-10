using Dry.Application.Contracts.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dry.Admin.Application.Contracts.Dtos
{
    /// <summary>
    /// 应用返回dto
    /// </summary>
    public class ApplicationDto : IResultDto
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }

    /// <summary>
    /// 应用查询dto
    /// </summary>
    public class ApplicationQueryDto : QueryDto<string>
    {
        /// <summary>
        /// 类型id
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// 名称模糊查询
        /// </summary>
        public string NameLike { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool? Enable { get; set; }
    }

    /// <summary>
    /// 应用新增dto
    /// </summary>
    public class ApplicationCreateDto : ICreateDto
    {
        /// <summary>
        /// 系统id
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 应用类型id
        /// </summary>
        [Required]
        [Range(0, 6)]
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Required]
        public bool Enable { get; set; }
    }

    /// <summary>
    /// 应用编辑dto
    /// </summary>
    public class ApplicationEditDto : IEditDto
    {
        /// <summary>
        /// 应用类型id
        /// </summary>
        public ValueDto<int> Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public ValueDto<string> Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public ValueDto<string> Url { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public ValueDto<string> Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public ValueDto<bool> Enable { get; set; }
    }
}