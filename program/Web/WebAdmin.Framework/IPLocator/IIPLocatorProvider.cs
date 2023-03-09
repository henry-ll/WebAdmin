namespace WebAdmin.Framework.IPLocator;

/// <summary>
/// IP 地址定位服务
/// </summary>
public interface IIPLocatorProvider
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<string?> Locate(string ip);
}
