namespace WebAdmin.Framework.IPLocator;

/// <summary>
/// IP 地址定位接口
/// </summary>
public interface IIPLocator
{
    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="option">定位器配置信息</param>
    /// <returns>定位器定位结果</returns>
    Task<string?> Locate(IPLocatorOption option);

    /// <summary>
    /// 获得/设置 接口地址
    /// </summary>
    public string? Url { get; set; }
}
