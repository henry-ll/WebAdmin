using Microsoft.Extensions.DependencyInjection;
using WebAdmin.Framework.IPLocator;
using WebAdmin.Framework.Util;

namespace WebAdmin.Framework.Helper;
/// <summary>
/// 
/// </summary>
public static class LocatorHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IIPLocator CreateLocator(IServiceProvider provider)
    {
        var dictService = provider.GetRequiredService<IDict>();
        var providerName = dictService.GetIpLocatorName();
        var providerUrl = dictService.GetIpLocatorUrl(providerName ?? string.Empty);

        return providerName switch
        {
            "BaiDuIPSvr" => new BaiDuIPLocator(),
            "JuheIPSvr" => new BaiDuIPLocator(),
            "BaiDuIP138Svr" => new BaiDuIPLocator() { Url = providerUrl },
            _ => new DefaultIPLocator()
        };
    }
}
