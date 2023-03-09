namespace WebAdmin.Mvc.Controllers.Basics
{
    /// <summary>
    /// 首页
    /// </summary>
    [SingleSignOnFilter(LoginMode.PCWebMvc)]
    public class HomeController : MvcControllerBase
    {
        private IMapper _mapper;
        private ILogger _logger;
        private IIPLocatorProvider _ipLocatorProvider;

        private ILoginlogsService _loginlogsService;
        /// <summary>
        /// 初始化 依赖注入
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="ipLocatorProvider"></param>
        public HomeController(IMapper mapper, ILogger logger, IIPLocatorProvider IPLocatorProvider, ILoginlogsService loginlogsService) : base(IPLocatorProvider)
        {
            _mapper = mapper;
            _logger = logger;
            _ipLocatorProvider = IPLocatorProvider;
            _loginlogsService = loginlogsService;
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Page()
        {
            return View();
        }

        /// <summary>
        /// 全局样式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 安全退出系统
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SafeLogout()
        {
            var userId = OperatorProvider.Provider.MvcCurrent()?.UserId;
            var account = OperatorProvider.Provider.MvcCurrent()?.Account;
            var userName = OperatorProvider.Provider.MvcCurrent()?.UserName;
            //清除当前会话
            OperatorProvider.Provider.EmptyCurrent();    //清除当前用户的所有Cookie、Seeion
            _loginlogsService.SaveLog(userId, account, userName, Ip, City, HostName, OS, Browser, User_Agent, "安全退出系统成功");
            return new RedirectResult("/Login/Index");
        }
    }
}