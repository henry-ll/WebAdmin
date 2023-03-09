namespace WebAdmin.Api.Controllers.BaseSystem;

/// <summary>
/// 测试接口
/// </summary>
[ApiController]
[ApiExplorerSettings(GroupName = "test")]
[SingleSignOnFilter(LoginMode.PCWebApi)]
//[JwtAuthorizeFilter(LoginMode.PCWebApi)]
public class TestController : ApiControllerBase
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
    public TestController(IMapper mapper, IUserService userService, ILogger logger)
    {
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
    }

    #region Get

    #endregion

    #region Post
    /// <summary>
    /// 测试分页
    /// </summary>
    /// <param name="pagination">参数</param>
    /// <returns></returns>
    [HttpPost]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResult))]
    public async Task<IActionResult> Test(Pagination pagination)
    {
        if (string.IsNullOrWhiteSpace(pagination.OrderFiled))
        {
            pagination.OrderFiled = "CreateDate";
        }
        if (string.IsNullOrWhiteSpace(pagination.OrderByType))
        {
            pagination.OrderByType = "ASC";
        }
        var result = await _userService.GetPagedListAsync(pagination);
        return ISuccess(result, "登录成功");
    }
    #endregion
}

