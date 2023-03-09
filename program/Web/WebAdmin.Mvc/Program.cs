var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsystem.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", true, reloadOnChange: true)
    .AddEnvironmentVariables().Build();

ConfigureServices();
var app = builder.Build();
Configure();
app.Run();

#region Services 配置
void ConfigureServices()
{
    //初始化配置
    builder.Services.InitConfiguration(Directory.GetCurrentDirectory(), Configuration);
    //初始化Serilog日志
    builder.Host.UseSerilog();
    //注入SeriLog
    builder.Services.AddSingleton(Log.Logger);
    //处理中文被html编码
    builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    //实时更新html页面
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddHttpContextAccessor();
    //拓展DeveloperExceptionPage
    builder.Services.AddSingleton<IDeveloperPageExceptionFilter, FakeExceptionFilter>();
    //配置全局获取HttpContext
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //Ip定位服务
    builder.Services.TryAddScoped<IIPLocatorProvider, DefaultIPLocatorProvider>();
    // 配置地理位置定位器
    builder.Services.ConfigureIPLocatorOption(options =>{options.LocatorFactory = provider => new BaiDuIPLocator();});
    // 配置地理位置定位器(未实现)
    //builder.Services.ConfigureIPLocatorOption(op => op.LocatorFactory = LocatorHelper.CreateLocator);
    //配置系统配置
    builder.Services.Configure<AppSystem>(Configuration.GetSection(nameof(AppSystem)));
    ConfigServiceProvider.Init(builder.Services);
    //ConfigServiceProvider.ConfigProvider = builder.Services.BuildServiceProvider();
    AppHttpContext.Configure(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
    //配置多个静态文件夹访问权限
    builder.Services.AddManyStaticFiles(builder, Configuration);
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });
    // Add services to the container.
    builder.Services.AddJwt(builder);
    builder.Services.AddMemoryCache();  //MemoryCache缓存
    builder.Services.AddResponseCaching();//缓存
    //解决返回结果时中文被编码
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });

#if DEBUG
#else
    //全局过滤器
    builder.Services.AddControllersWithViews(option =>
    {
        option.Filters.Add(typeof(GlobalMvcExceptionFilter));//全局异常过滤器
    });
#endif
    //设置跨域
    builder.Services.AddApiCors(builder);
    //autoMapper 对象自动映射
    builder.Services.AddAutoMapperProfile();
    //将HttpClient注入进来
    builder.Services.AddHttpClient();
    //添加Session
    builder.Services.AddSession(options =>
    {
        // 设置 Session 过期时间
        options.IdleTimeout = TimeSpan.FromMinutes(120);
        options.Cookie.HttpOnly = true;
    });
    //注册Cookie认证服务
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
        CookieAuthenticationDefaults.AuthenticationScheme, option =>
        {
            option.LoginPath = "/Login/Index";
            option.Cookie.Name = "TokenCookie";//设置存储用户登录信息（用户Token信息）的Cookie名称
            option.Cookie.HttpOnly = true;//设置存储用户登录信息（用户Token信息）的Cookie，无法通过客户端浏览器脚本(如JavaScript等)访问到
            option.Cookie.SecurePolicy = CookieSecurePolicy.None;//设置存储用户登录信息（用户Token信息）的Cookie，只会通过HTTPS协议传递，如果是HTTP协议，Cookie不会被发送。注意，option.Cookie.SecurePolicy属性的默认值是Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest
        });

    Assembly[] array = new Assembly[]
    {
        Assembly.Load("WebAdmin.Infrastructure"),
        Assembly.Load("WebAdmin.Service"),
        Assembly.Load("WebAdmin.Repositories"),
    };
    //自动注入
    builder.Services.AutoInjection(AutoInjectionExtention.InjectionType.scope, array);
    //MemoryCache缓存
    builder.Services.AddMemoryCache();
    //Response缓存
    builder.Services.AddResponseCaching();
    //压缩Response
    builder.Services.AddResponseCompression();
    builder.Services.AddRazorPages();
}
#endregion

#region app 配置
void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    //ASP.NET Core强制执行HTTPS
    app.UseHsts();
    app.UseHttpsRedirection();
    //默认文件中间件 wwwroot Index.htm Index.html default.htm default.html
    app.UseDefaultFiles();
    //启用多个静态文件夹访问
    app.UseManyStaticFiles(Configuration);
    app.UseRouting();
    //配置Cors跨域
    app.UseCors();
    //认证
    app.UseAuthentication();
    //授权
    app.UseAuthorization();
    app.UseSession();
    app.UseCookiePolicy();
    app.UseResponseCompression();
    app.UseResponseCaching();
    //校验Get/Post传入参数中间件
    app.UseParamsMiddleware();
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
}
#endregion
