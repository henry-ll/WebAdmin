using Serilog;
using Microsoft.Extensions.Options;

namespace WebAdmin.Framework.IPLocator;

/// <summary>
/// 默认 IP 地理位置定位器
/// </summary>
public class DefaultIPLocatorProvider : IIPLocatorProvider
{
    private readonly IPLocatorOption _option;

    private readonly IServiceProvider _provider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="factory"></param>
    /// <param name="logger"></param>
    /// <param name="option"></param>
    public DefaultIPLocatorProvider(IServiceProvider provider, IHttpClientFactory factory, ILogger logger, IOptionsMonitor<IPLocatorOption> option)
    {
        _provider = provider;
        _option = option.CurrentValue;
        _option.HttpClient = factory.CreateClient();
        _option.Logger = logger;
    }

    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<string?> Locate(string ip)
    {
        string? ret = null;

        // 解析本机地址
        if (string.IsNullOrEmpty(ip) || _option.Localhosts.Any(p => p == ip))
        {
            ret = "本地连接";
        }
        else
        {
            // IP定向器地址未设置
            _option.IP = ip;
            if (_option.LocatorFactory != null)
            {
                var locator = _option.LocatorFactory(_provider);
                if (locator != null)
                {
                    ret = await locator.Locate(_option);
                }
            }
        }
        return ret;
    }
}
