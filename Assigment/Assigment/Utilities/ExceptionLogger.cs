using Assigment;
using Serilog;

namespace Assigment.Utilities
{
    public class ExceptionLogger
    {
        public ExceptionLogger(string domainId)
        {
            string tableName = Startup.LogTableName;
            string con = Startup.Connectionstring;

            switch (Startup.LogLevel.ToUpper())
            {
                case "DEBUG":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.WithExceptionData()
                        .Enrich.WithExceptionStackTraceHash()
                        .Enrich.WithDemystifiedStackTraces()
                        .Enrich.WithProperty("UserName", domainId ?? string.Empty)
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        //.WriteTo.MSSqlServer(con, tableName, columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions())
                        .WriteTo.File("Logs\\log.txt", buffered: false, rollingInterval: RollingInterval.Minute, rollOnFileSizeLimit: false)
                        .CreateLogger();
                    break;
                case "ERROR":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.WithExceptionData()
                        .Enrich.WithExceptionStackTraceHash()
                        .Enrich.WithDemystifiedStackTraces()
                        .Enrich.WithProperty("UserName", domainId ?? string.Empty)
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        //.WriteTo.MSSqlServer(con, tableName, columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions())
                        .WriteTo.File("Logs\\log.txt", buffered: false, rollingInterval: RollingInterval.Minute, rollOnFileSizeLimit: false)
                        .CreateLogger();
                    break;
                case "VERBOSE":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.WithExceptionData()
                        .Enrich.WithExceptionStackTraceHash()
                        .Enrich.WithDemystifiedStackTraces()
                        .Enrich.WithProperty("UserName", domainId ?? string.Empty)
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        //.WriteTo.MSSqlServer(con, tableName, columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions())
                        .WriteTo.File("Logs\\log.txt", buffered: false, rollingInterval: RollingInterval.Minute, rollOnFileSizeLimit: false)
                        .CreateLogger();
                    break;
                case "FATAL":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.WithExceptionData()
                        .Enrich.WithExceptionStackTraceHash()
                        .Enrich.WithDemystifiedStackTraces()
                        .Enrich.WithProperty("UserName", domainId ?? string.Empty)
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        //.WriteTo.MSSqlServer(con, tableName, columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions())
                        .WriteTo.File("Logs\\log.txt", buffered: false, rollingInterval: RollingInterval.Minute, rollOnFileSizeLimit: false)
                        .CreateLogger();
                    break;
                default:
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.WithExceptionData()
                        .Enrich.WithExceptionStackTraceHash()
                        .Enrich.WithDemystifiedStackTraces()
                        .Enrich.WithProperty("UserName", domainId ?? string.Empty)
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        //.WriteTo.MSSqlServer(con, tableName, columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions())
                        .WriteTo.File("Logs\\log.txt", buffered: false, rollingInterval: RollingInterval.Minute, rollOnFileSizeLimit: false)
                        .CreateLogger();
                    break;
            }
        }
    }
}
