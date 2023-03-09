namespace WebAdmin.Api.Controllers.System;

/// <summary>
/// 登录接口
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(GroupName = "basesystem")]
public class LoginController : ApiControllerBase
{
    private IMapper _mapper;
    private ILogger _logger;
    private IUserService _userService;
    /// <summary>
    /// 初始化 依赖注入
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userService"></param>
    /// <param name="logger"></param>
    public LoginController(IMapper mapper, IUserService userService, ILogger logger)
    {
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
    }

    #region Get
    /// <summary>
    ///登陆验证码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VierificeCode))]
    public async Task<IActionResult> GetVierificationCode()
    {
        string code = VierificationCode.RandomText();
        var result = new VierificeCode()
        {
            ImgBase64 = VierificationCode.CreateBase64Imgage(code),
            uuid = Guid.NewGuid().ToString(),
#if DEBUG
            VerificationCode = code.ToLower(),
#else
#endif
        };
        ICache cache = CacheFactory.Cache();//自带缓存
        cache.WriteCache(code.ToLower(), result.uuid.ToString(), DateTime.Now.AddMinutes(10));//10分钟后自动过期
        //ICacheByRedis cacheByRedis = CacheFactory.CacheByRedis();//Redis缓存
        //cacheByRedis.Write(result.uuid.ToString(), code.ToLower(), DateTime.Now.AddMinutes(10));//10分钟后自动过期
        return ISuccess(result);
    }
    #endregion

    #region Post
    /// <summary>
    /// 登陆
    /// </summary>
    /// <param name="model">参数</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResult))]
    public IActionResult UserLogin(LoginDto param)
    {
        if (string.IsNullOrWhiteSpace(param.Account) || string.IsNullOrWhiteSpace(param.PassWord))
            return IError("账号或密码错误");
        if (string.IsNullOrWhiteSpace(param.uuid))
            return IError("验证码错误");
        if (string.IsNullOrWhiteSpace(param.VerificeCode))
            return IError("验证码不能为空");
        ICache cache = CacheFactory.Cache();
        string code = cache.GetCache<string>(param.uuid);

        //ICacheByRedis cacheByRedis = CacheFactory.CacheByRedis();
        //string code = cacheByRedis.Read<string>(param.uuid);

        if (string.IsNullOrWhiteSpace(code))
            return IError("验证码已过期");
        if (param.VerificeCode.ToLower().Trim() != code.ToLower())
            return IError("验证码输入有误！");

        cache.RemoveCache(param.uuid);
        //cacheByRedis.Remove(param.uuid);

        UserEntity user = _userService.CheckLogin(param.Account, param.PassWord);
        JwtOperator operators = new JwtOperator();
        operators.UserId = user.Id;
        operators.Account = user.Account;
        operators.UserName = user.RealName;
        operators.SignInTime = DateTime.Now;
        operators.LoginMode = (int)LoginMode.PCWebApi;
        //判断是否系统管理员
        if (user.Account == "System")
            operators.IsSystem = true;

        var date = DateTime.Now;
        string Jwttoken = JwtHelper.IssueJwt(operators, date);
        operators.JwtToken = Jwttoken;

        LoginResult result = new LoginResult();
        result.UserId = user.Id;
        result.UserName = user.RealName;
        result.SignInTime = date;
        result.JwtToken = user.JwtToKen;
        //判断是否系统管理员
        if (user.Account == "System")
            result.IsSystem = true;
        result.JwtToken = Jwttoken;

        user.JwtToKen = Jwttoken;
        _userService.ApiSaveForm(user.Id, user);

        var cacheKey = "UserLogin_" + user.Id;
        cache.RemoveCache(cacheKey);
        cache.WriteCache(user.JwtToKen, cacheKey, date.AddMinutes(AppSetting.JwtExpMinutes));

        //cacheByRedis.Remove(cacheKey);
        //cacheByRedis.Write(cacheKey, user.JwtToKen, date.AddMinutes(AppSetting.JwtExpMinutes));

        return ISuccess(result, "登录成功");
    }
    #endregion

}
