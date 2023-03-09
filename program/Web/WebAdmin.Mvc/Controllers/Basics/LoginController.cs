namespace WebAdmin.Mvc.Controllers.Basics;

public class LoginController : MvcControllerBase
{
    private IMapper _mapper;
    private ILogger _logger;
    private IIPLocatorProvider _ipLocatorProvider;

    private ILoginlogsService _loginlogsService;
    private IUserService _userService;
    /// <summary>
    /// 初始化 依赖注入
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="loginlogsService"></param>
    public LoginController(IMapper mapper, ILogger logger, IIPLocatorProvider IPLocatorProvider, ILoginlogsService loginlogsService, IUserService userService) : base(IPLocatorProvider)
    {
        _mapper = mapper;
        _logger = logger;
        _loginlogsService = loginlogsService;
        _ipLocatorProvider = IPLocatorProvider;
        _userService = userService;
    }
    /// <summary>
    /// 登录页面
    /// </summary>
    /// <param name="messtype">弹窗消息提示（-1弹窗不弹出，1弹窗弹出）</param>
    /// <returns></returns>
    public IActionResult Index(int messtype = -1)
    {
        ViewBag.Messtype = messtype;
        return View();
    }

    #region Get

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult VerifyCode()
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
        return Success(result);
    }

    #endregion

    #region Post
    /// <summary>
    /// 账号密码登录
    /// </summary>
    /// <param name="username">账号</param>
    /// <param name="md5_password">密码</param>
    /// <param name="uuid">uuid</param>
    /// <param name="verifyCode">验证码</param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public IActionResult CheckLogin(LoginDto param)
    {
        if (string.IsNullOrWhiteSpace(param.username) || string.IsNullOrWhiteSpace(param.md5_password))
            return Error("账号或密码错误");
        if (string.IsNullOrWhiteSpace(param.uuid))
            return Error("验证码错误");
        if (string.IsNullOrWhiteSpace(param.verifyCode))
            return Error("验证码不能为空");
        ICache cache = CacheFactory.Cache();
        string code = cache.GetCache<string>(param.uuid);
        if (string.IsNullOrWhiteSpace(code))
            return Error("验证码已过期");
        if (param.verifyCode.ToLower()!= code.ToLower())
            return Error("验证码输入有误");
        cache.RemoveCache(param.uuid);
        CryptoHelper crypto = new CryptoHelper();
        try
        {
            var model = _userService.Queryable().Where(x => x.Account == param.username).First();
            if (model == null)
                return Error("账号或密码错误");
            UserEntity userEntity = null;
            try
            {
                userEntity = _userService.CheckLogin(param.username, param.md5_password);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _loginlogsService.SaveLog(model.Id, model.Account, model.RealName, Ip, City, HostName, OS, Browser, User_Agent, false);
                return Error("登录失败");
            }

            JwtOperator operators = new JwtOperator();
            operators.UserId = userEntity.Id;
            operators.UserName = userEntity.RealName;
            operators.OrganizeId = userEntity.OrganizeId;
            operators.SignInTime = DateTime.Now;
            operators.Account = userEntity.Account;
            operators.LoginMode = (int)LoginMode.PCWebMvc;
            //判断是否系统管理员
            if (userEntity.Account == "System")
                operators.IsSystem = true;
            else
                operators.IsSystem = false;

            string token = JwtHelper.IssueJwt(operators);
            operators.JwtToken = token;
            userEntity.JwtToKen = token;
            _userService.MvcSaveForm(userEntity.Id, userEntity,false);
            //写入日志
            _loginlogsService.SaveLog(model.Id, model.Account, model.RealName, Ip, City, HostName, OS, Browser, User_Agent, true);
            OperatorProvider.Provider.AddMvcCurrent(operators,60 * 24 * 7);
            return Success(operators, "登录成功");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Error("出现错误");
        }
    }
    #endregion
}
