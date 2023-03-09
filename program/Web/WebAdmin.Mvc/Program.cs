var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsystem.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", true, reloadOnChange: true)
    .AddEnvironmentVariables().Build();

ConfigureServices();
var app = builder.Build();
Configure();
app.Run();

#region Services ����
void ConfigureServices()
{
    //��ʼ������
    builder.Services.InitConfiguration(Directory.GetCurrentDirectory(), Configuration);
    //��ʼ��Serilog��־
    builder.Host.UseSerilog();
    //ע��SeriLog
    builder.Services.AddSingleton(Log.Logger);
    //�������ı�html����
    builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    //ʵʱ����htmlҳ��
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddHttpContextAccessor();
    //��չDeveloperExceptionPage
    builder.Services.AddSingleton<IDeveloperPageExceptionFilter, FakeExceptionFilter>();
    //����ȫ�ֻ�ȡHttpContext
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //Ip��λ����
    builder.Services.TryAddScoped<IIPLocatorProvider, DefaultIPLocatorProvider>();
    // ���õ���λ�ö�λ��
    builder.Services.ConfigureIPLocatorOption(options =>{options.LocatorFactory = provider => new BaiDuIPLocator();});
    // ���õ���λ�ö�λ��(δʵ��)
    //builder.Services.ConfigureIPLocatorOption(op => op.LocatorFactory = LocatorHelper.CreateLocator);
    //����ϵͳ����
    builder.Services.Configure<AppSystem>(Configuration.GetSection(nameof(AppSystem)));
    ConfigServiceProvider.Init(builder.Services);
    //ConfigServiceProvider.ConfigProvider = builder.Services.BuildServiceProvider();
    AppHttpContext.Configure(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
    //���ö����̬�ļ��з���Ȩ��
    builder.Services.AddManyStaticFiles(builder, Configuration);
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });
    // Add services to the container.
    builder.Services.AddJwt(builder);
    builder.Services.AddMemoryCache();  //MemoryCache����
    builder.Services.AddResponseCaching();//����
    //������ؽ��ʱ���ı�����
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });

#if DEBUG
#else
    //ȫ�ֹ�����
    builder.Services.AddControllersWithViews(option =>
    {
        option.Filters.Add(typeof(GlobalMvcExceptionFilter));//ȫ���쳣������
    });
#endif
    //���ÿ���
    builder.Services.AddApiCors(builder);
    //autoMapper �����Զ�ӳ��
    builder.Services.AddAutoMapperProfile();
    //��HttpClientע�����
    builder.Services.AddHttpClient();
    //���Session
    builder.Services.AddSession(options =>
    {
        // ���� Session ����ʱ��
        options.IdleTimeout = TimeSpan.FromMinutes(120);
        options.Cookie.HttpOnly = true;
    });
    //ע��Cookie��֤����
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
        CookieAuthenticationDefaults.AuthenticationScheme, option =>
        {
            option.LoginPath = "/Login/Index";
            option.Cookie.Name = "TokenCookie";//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie����
            option.Cookie.HttpOnly = true;//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie���޷�ͨ���ͻ���������ű�(��JavaScript��)���ʵ�
            option.Cookie.SecurePolicy = CookieSecurePolicy.None;//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie��ֻ��ͨ��HTTPSЭ�鴫�ݣ������HTTPЭ�飬Cookie���ᱻ���͡�ע�⣬option.Cookie.SecurePolicy���Ե�Ĭ��ֵ��Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest
        });

    Assembly[] array = new Assembly[]
    {
        Assembly.Load("WebAdmin.Infrastructure"),
        Assembly.Load("WebAdmin.Service"),
        Assembly.Load("WebAdmin.Repositories"),
    };
    //�Զ�ע��
    builder.Services.AutoInjection(AutoInjectionExtention.InjectionType.scope, array);
    //MemoryCache����
    builder.Services.AddMemoryCache();
    //Response����
    builder.Services.AddResponseCaching();
    //ѹ��Response
    builder.Services.AddResponseCompression();
    builder.Services.AddRazorPages();
}
#endregion

#region app ����
void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    //ASP.NET Coreǿ��ִ��HTTPS
    app.UseHsts();
    app.UseHttpsRedirection();
    //Ĭ���ļ��м�� wwwroot Index.htm Index.html default.htm default.html
    app.UseDefaultFiles();
    //���ö����̬�ļ��з���
    app.UseManyStaticFiles(Configuration);
    app.UseRouting();
    //����Cors����
    app.UseCors();
    //��֤
    app.UseAuthentication();
    //��Ȩ
    app.UseAuthorization();
    app.UseSession();
    app.UseCookiePolicy();
    app.UseResponseCompression();
    app.UseResponseCaching();
    //У��Get/Post��������м��
    app.UseParamsMiddleware();
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
}
#endregion
