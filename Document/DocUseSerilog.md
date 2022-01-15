# Writing logs to different files using Serilog in ASP.NET Core Web Application

* by: [Writing Logs using Serilog in ASP.NET](https://www.techrepository.in/blog/posts/writing-logs-to-different-files-serilog-asp-net-core)

## 1. **Adding Serilog**

- To integrate Serilog with our application, we will need to add the following packages
    * [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore) - Base package dedicated for ASP .NET Core integration
    * [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration) - Provider that reads from the Configuration object
    * [Serilog.Filters.Expressions](https://github.com/serilog/serilog-filters-expressions) - Used for expression-based event filtering
    * [Serilog.Sinks.File](https://github.com/serilog/serilog-sinks-file) - Used for writing logs in to one or more files
- To add the packages into your projects, execute the commands given below from the command prompt
    
    > `dotnet` add package Serilog.AspNetCore

    > `dotnet` add package Serilog.Settings.Configuration

    > `dotnet` add package Serilog.Filters.Expressions

    > `dotnet` add package Serilog.Sinks.File
## 2. **Added Serilog settings in the configuration file**
* Now we will modify our `appsettings.json` file to add the settings for Serilog. In one of the earlier step, we removed the default entries for logging and will add the following instead of that
```json
{
    "Serilog": {
    "MinimumLevel": {
    "Default": "Debug",
    "Override": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
    }
    },
    "WriteTo": [
    {
        "Name": "Logger",
        "Args": {
        "configureLogger": {
            "Filter": [
            {
                "Name": "ByIncludingOnly",
                "Args": {
                "expression": "(@Level = 'Error' or @Level = 'Fatal' or @Level = 'Warning')"
                }
            }
            ],
            "WriteTo": [
            {
                "Name": "File",
                "Args": {
                "path": "Logs/ex_.log",
                "outputTemplate": "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}",
                "rollingInterval": "Day",
                "retainedFileCountLimit": 7
                }
            }
            ]
        }
        }
    },
    {
        "Name": "Logger",
        "Args": {
        "configureLogger": {
            "Filter": [
            {
                "Name": "ByIncludingOnly",
                "Args": {
                "expression": "(@Level = 'Information' or @Level = 'Debug')"
                }
            }
            ],
            "WriteTo": [
            {
                "Name": "File",
                "Args": {
                "path": "Logs/cp_.log",
                "outputTemplate": "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}",
                "rollingInterval": "Day",
                "retainedFileCountLimit": 7
                }
            }
            ]
        }
        }
    }
    ],
    "Enrich": [
    "FromLogContext",
    "WithMachineName"
    ],
    "Properties": {
    "Application": "MultipleLogFilesSample"
    }
}
}
```
Here we have configured entries for writing logs into two files depending upon the severity. For the first one, we set up the filter like this
```json
"configureLogger": {
    "Filter": [
        {
        "Name": "ByIncludingOnly",
        "Args": {
            "expression": "(@Level = 'Error' or @Level = 'Fatal' or @Level = 'Warning')"
        }
        }
    ],
```
Because of this condition, only `Error`, `Fatal` and `Warning` types of logs will be written into a file named `ex_*.log`. Since we have configured it as a rolling file, the date will also get appended to the file name. Similarly, for the second one, it will write into the file named `cp_*.log` if and only if the level is `Debug` or `Information`

## 3. **Integrate Serilog in the application**
To configure Serilog in our application, we will modify our `Program.cs` file to call the Serilog middleware as shown below
```cs
public static IHostBuilder CreateHostBuilder(string[] args)=>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .UseSerilog((hostingContext, loggerConfig) =>
        loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
        );
```

## 3. **Writing logs from the application**
Let’s modify the `Index` method in the `HomeController.cs` file to simulate the call to logger methods which in turns writes information to the files
`test`
```cs
public HomeController(ILogger<HomeController> logger)
{
    _logger = logger;
    _logger.LogInformation("Writing to log file with INFORMATION severity level.");
    _logger.LogDebug("Writing to log file with DEBUG severity level."); 
    _logger.LogWarning("Writing to log file with WARNING severity level.");
    _logger.LogError("Writing to log file with ERROR severity level.");
    _logger.LogCritical("Writing to log file with CRITICAL severity level.");
}
```
If you run the application now, you will see the information in written into different files based on the log levels

- File `cp_*.log` 

        2020-06-20T23:11:42.0922074+05:30 [INF](MultipleLogFilesSample.Controllers.HomeController) This is a log with INFORMATION severity level.

        2020-06-20T23:11:42.0934669+05:30 [DBG](MultipleLogFilesSample.Controllers.HomeController) This is a log with DEBUG severity level.


- File `ex_*.log`

        2020-06-20T23:11:42.0934979+05:30 [WRN](MultipleLogFilesSample.Controllers.HomeController) This is a log with WARNING severity level.

        2020-06-20T23:11:42.0950269+05:30 [ERR](MultipleLogFilesSample.Controllers.HomeController) This is a log with ERROR severity level.

        2020-06-20T23:11:42.0958420+05:30 [FTL](MultipleLogFilesSample.Controllers.HomeController) This is a log with CRITICAL severity level.

![image](https://f4-zpcloud.zdn.vn/2869947440173736092/6beceb1fa00b6a55331a.jpg "example")