using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DK_API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureKestrel(kestrelServerOptions =>
                    //{

                    // Thiết lập lắng nghe trên cổng 8090 với IP bất kỳ
                    //    kestrelServerOptions.Listen(IPAddress.Any, 8090);
                    //});

                    // Thiết lập lắng nghe trên cổng 8090 với IP bất kỳ
                    // webBuilder.UseKestrel(kestrelServerOptions =>
                    // {
                    //     // Các thiết lập cho Kestrel tại đây
                    //     // sử dụng KestrelServerOptons để thiết lập
                    //     // Thiết lập lắng nghe trên cổng 8090 với IP bất kỳ
                    //     kestrelServerOptions.Listen(IPAddress.Any, 8090);
                    // });
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://localhost:5000");
                })
                .UseSerilog((hostingContext, loggerConfig) => // configure Serilog in our application
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                );
    }
}
