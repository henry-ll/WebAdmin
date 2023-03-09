namespace WebAdmin.Framework.Util;

/// <summary>
/// Dict 字典表接口
/// </summary>
public interface IDict
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string? GetIpLocatorName();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    string? GetIpLocatorUrl(string? name);
    /// <summary>
    /// 获得地理位置服务
    /// </summary>
    /// <returns></returns>
    Dictionary<string, string> GetIpLocators();

    /// <summary>
    /// 获得当前地理位置服务
    /// </summary>
    /// <returns></returns>
    string? GetIpLocator();

    /// <summary>
    /// 设置当前地理位置服务
    /// </summary>
    /// <returns></returns>
    bool SaveCurrentIp(string value);

    /// <summary>
    /// 获得 IP 请求缓存时长
    /// </summary>
    /// <returns></returns>
    int GetIPCacheExpired();

    /// <summary>
    /// 设置 IP 请求缓存时长
    /// </summary>
    /// <returns></returns>
    bool SaveIPCacheExpired(int value);

}
