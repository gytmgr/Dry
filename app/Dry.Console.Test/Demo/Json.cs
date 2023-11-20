using Dry.Core.Model;
using Dry.Core.Utilities;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dry.Console.Test.Demo;

public static class Json
{
    public static Task Run()
    {
        var options = new JsonSerializerOptions().DefaultConfig();
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
        options.Converters.Add(new StructJsonConverter<bool>());
        options.Converters.Add(new StructNullableJsonConverter<bool>());
        var objs = JsonSerializer.Deserialize<GG>("{\"QQ\":16156}", options);

        System.Console.ReadKey();


        options = new JsonSerializerOptions();
        options.Converters.Add(new StructJsonConverter<decimal>());
        options.Converters.Add(new StructNullableJsonConverter<decimal>());
        options.Converters.Add(new StructJsonConverter<int>());
        options.Converters.Add(new StructNullableJsonConverter<int>());
        var json = JsonSerializer.Serialize(new GG { TT = (decimal)1.2, EE = 5 }, options);
        var gg = JsonSerializer.Deserialize<GG>(json, options);
        System.Console.ReadKey();

        return Task.CompletedTask;
    }
}

public class GG : ISingletonDependency<GG>
{
    public bool QQ { get; set; }
    public int? WW { get; set; }
    public int EE { get; set; }
    public decimal? RR { get; set; }
    public decimal TT { get; set; }
}