﻿using Dry.Core.Utilities;
using System;
using System.Text.Json;

namespace Dry.Json.Converter
{
    /// <summary>
    /// 结构转换
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public class StructJsonConverter<TStruct> : DryJsonConverter<TStruct> where TStruct : struct
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override TStruct Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => (reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt64().ToString(),
                JsonTokenType.True or JsonTokenType.False => reader.GetBoolean().ToString(),
                _ => reader.GetString()
            }).TryParse<TStruct>(out var value) ? value : default;
    }

    /// <summary>
    /// 可为空结构转换
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public class StructNullableJsonConverter<TStruct> : DryJsonConverter<TStruct?> where TStruct : struct
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns> 
        public override TStruct? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => (reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt64().ToString(),
                JsonTokenType.True or JsonTokenType.False => reader.GetBoolean().ToString(),
                _ => reader.GetString()
            }).TryParse<TStruct>(out var value) ? value : null;
    }
}