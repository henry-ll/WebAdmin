using X.PagedList;

namespace WebAdmin.Mvc.Controllers.Base
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [SingleSignOnFilter(LoginMode.PCWebMvc)]
    public class UserController : MvcControllerBase
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
        /// <param name="ipLocatorProvider"></param>
        public UserController(IMapper mapper, ILogger logger, IIPLocatorProvider IPLocatorProvider, ILoginlogsService loginlogsService, IUserService userService) : base(IPLocatorProvider)
        {
            _mapper = mapper;
            _logger = logger;
            _ipLocatorProvider = IPLocatorProvider;
            _loginlogsService = loginlogsService;
            _userService = userService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(Pagination pagination)
        {
            pagination.OrderFiled = "CreateDate";
            pagination.OrderByType = "DESC";
            var list =await _userService.GetPagedListAsync(pagination);
            ViewBag.Pagination = pagination;
            return View(list);
        }
    }
}
