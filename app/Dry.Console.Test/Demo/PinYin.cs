using Dry.PinYin;
using System.Threading.Tasks;

namespace Dry.Console.Test.Demo;

public static class PinYin
{
    public static Task Run()
    {
        var gg = PinYinConverter.Get("生产厂家");
        var cc = PinYinHelper.GetTotalPingYin("生产厂家");

        System.Console.ReadKey();
        return Task.CompletedTask;
    }
}