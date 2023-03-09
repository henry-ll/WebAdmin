using WebAdmin.Repositories.Base;
using X.PagedList;

namespace WebAdmin.Service.Base;

/// <summary>
/// 用户管理
/// </summary>
public class UserService : BaseService<UserEntity>, IUserService
{
    /// <summary>
    /// 
    /// </summary>
    public UserRepository _userRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="userRepository"></param>
    public UserService(UserRepository userRepository):base(userRepository)
    {
        _userRepository = userRepository;
    }
    /// <summary>
    /// 无参数构造函数
    /// </summary>
    public UserService() : base(new UserRepository())
    {
        _userRepository = new UserRepository();
    }
    /// <summary>
    /// 缓存key
    /// </summary>
    public string cacheKey = "userCache";

    #region 获取数据
    /// <summary>
    /// 根据token查询用户
    /// </summary>
    /// <param name="token">token</param>
    /// <returns></returns>
    public UserEntity GetEntityByToken(string token)
    {
        var result = _userRepository.GetEntityByToken(token);
        return result;
    }
    /// <summary>
    /// 根据条件查询用户列表
    /// </summary>
    /// <param name="queryJson">查询条件</param>
    /// <returns></returns>
    public IEnumerable<UserEntity> GetList(string queryJson)
    {
        var result = _userRepository.GetList(queryJson);
        return result;
    }
    /// <summary>
    /// 根据条件查询用户分页列表
    /// </summary>
    /// <param name="pagination">分页参数</param>
    /// <returns></returns>
    public IPagedList<UserEntity> GetPagedList(Pagination pagination)
    {
        var result = _userRepository.GetPagedList(pagination);
        return result;
    }
    /// <summary>
    /// 根据条件查询用户分页列表
    /// </summary>
    /// <param name="pagination">分页参数</param>
    /// <returns></returns>
    public async Task<IPagedList<UserEntity>> GetPagedListAsync(Pagination pagination)
    {
        var result =await _userRepository.GetPagedListAsync(pagination);
        return result;
    }
    #endregion

    #region 验证数据
    /// <summary>
    /// 账户不能重复
    /// </summary>
    /// <param name="account">账户值</param>
    /// <param name="keyValue">主键</param>
    /// <returns></returns>
    public bool ExistAccount(string account, string keyValue = "")
    {
        var result = _userRepository.ExistAccount(account, keyValue);
        return result;
    }
    /// <summary>
    /// 到数据库中进行登录验证
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public UserEntity CheckLogin(string username, string password)
    {
        UserEntity userEntity = _userRepository.CheckLogin(username);
        if (userEntity != null)
        {
            if (userEntity.EnabledMark == 1)
            {
                string dbPassword = Md5Helper.MD5(DESEncrypt.Encrypt(password, userEntity.Secretkey).ToLower(), 32).ToLower();
                if (userEntity.LockEndDate >= DateTime.Now)
                {
                    _userRepository.ApiSaveForm(userEntity.Id, userEntity);
                    throw new BusinessException("用户名或密码五次输入错误，请30分钟后重试");
                }
                if (dbPassword == userEntity.Password)
                {
                    DateTime LastVisit = DateTime.Now;
                    int LogOnCount = (userEntity.LogOnCount).ToInt() + 1;
                    if (userEntity.LastVisit != null)
                        userEntity.PreviousVisit = userEntity.LastVisit.ToDate();
                    userEntity.LastVisit = LastVisit;
                    userEntity.LogOnCount = LogOnCount;
                    userEntity.LoginsNum = 0;
                    _userRepository.ApiSaveForm(userEntity.Id, userEntity);
                    return userEntity;
                }
                else
                {
                    userEntity.LoginsNum++;
                    if (userEntity.LoginsNum == 5)
                        userEntity.LockEndDate = DateTime.Now.AddHours(0.5);
                    _userRepository.ApiSaveForm(userEntity.Id, userEntity);
                    throw new BusinessException("用户名或密码错误，还有" + (5 - userEntity.LoginsNum) + "次机会");
                }
            }
            else
                throw new BusinessException("账户被系统锁定,请联系管理员");
        }
        else
            throw new BusinessException("用户名或密码错误，请重新输入");
    }
    #endregion

    #region 保存数据

    #endregion

}
