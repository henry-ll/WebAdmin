using Serilog;
using Serilog.Events;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System.Security.AccessControl;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.MSSqlServer;
using Serilog.Core;
using WebAdmin.Framework.Util;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SerilogExtension
    {
        /// <summary>
        /// Serilog日志模板
        /// </summary>
        static string serilogDebug = System.Environment.CurrentDirectory + "\\LogFiles\\Debug\\log.txt";
        static string serilogInfo = System.Environment.CurrentDirectory + "\\LogFiles\\Info\\log.txt";
        static string serilogWarn = System.Environment.CurrentDirectory + "\\LogFiles\\Warning\\log.txt";
        static string serilogError = System.Environment.CurrentDirectory + "\\LogFiles\\Error\\log.txt";
        static string serilogFatal = System.Environment.CurrentDirectory + "\\LogFiles\\Fatal\\log.txt";
        static string serilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";

        static string _connectionString = DatabaseConfig.baseDConnString;
        static string _schemaName = "dbo";
        static string _tableName = "Log_Serilogs";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder)
        {
            //将配置传递给 Serilog 的初始化过程
            Log.Logger = new LoggerConfiguration()
                //.ReadFrom.Configuration(SerilogConfiguration)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console() // 输出到Console控制台
                .WriteTo.Logger(lg =>
                lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: serilogOutputTemplate, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileTimeLimit: TimeSpan.FromDays(31), rollOnFileSizeLimit: true, retainedFileCountLimit: null,shared:true)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Day, outputTemplate: serilogOutputTemplate, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileTimeLimit: TimeSpan.FromDays(31), rollOnFileSizeLimit: true, retainedFileCountLimit: null, shared: true)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Day, outputTemplate: serilogOutputTemplate, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileTimeLimit: TimeSpan.FromDays(31), rollOnFileSizeLimit: true, retainedFileCountLimit: null, shared: true)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Day, outputTemplate: serilogOutputTemplate, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileTimeLimit: TimeSpan.FromDays(31), rollOnFileSizeLimit: true, retainedFileCountLimit: null, shared: true)))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Day, outputTemplate: serilogOutputTemplate, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileTimeLimit: TimeSpan.FromDays(31), rollOnFileSizeLimit: true, retainedFileCountLimit: null, shared: true)))
                .WriteTo.MSSqlServer(_connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = _tableName, SchemaName = _schemaName, AutoCreateSqlTable = true })//输出到sqlserver数据库
                .CreateLogger();
            // 使用Serilog
            // dispose 设置为 true，它就会在退出时自动释放日志对象
            hostBuilder.UseSerilog(dispose: true);
            return hostBuilder;
        }
    }
}
