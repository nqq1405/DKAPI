using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using DK_API.Repository.InterfaceRepo;
using DK_API.Setting;
using DK_API.Repository;
using DK_API.Repository.InfRepository;
using DK_API.Service;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;

namespace DK_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add DependencyInjection Type Singleton
            services.AddSingleton<IStationRepository, MongodbStationDataRepo>();
            services.AddSingleton<ICityRepository, MongodbCityRepo>();
            services.AddSingleton<IVehicleRepository, MongodbVehicleDataRepo>();
            services.AddSingleton<IPDBRepository, MongodbRoadCostDataRepo>();
            services.AddSingleton<IPKDRepository, MongodbRegistrationCostDataRepo>();
            services.AddSingleton<ISlotDataRepository, MongodbSlotDataRepo>();
            services.AddSingleton<IScheUserRepository, MongodbUserScheduleInfoRepo>();
            services.AddSingleton<IStationSlotInfoRepository, MongodbStationSlotInfoRepo>();
            services.AddSingleton<IStationSlotDataRepository, MongodbStationSlotDataRepo>();
            services.AddSingleton<IVoucherRepository, MongodbVoucherRepo>();
            services.AddSingleton<IUserServiceOrderRepository, MongodbUserServiceOrderRepo>();

            services.AddSingleton<UserService>();
            services.AddSingleton<YearService>();
            services.AddSingleton<MappingProfile>();
            services.AddSingleton<SlotService>();
            services.AddSingleton<StationService>();
            services.AddSingleton<VoucherService>();
            services.AddSingleton<MongodbIpGeoLocation>();

            // MongoDb Context setting
            var MongoDbsettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                return new MongoClient(MongoDbsettings.ConnectionString);
            });
            // Delete SuppressAsuncSuffix
            services.AddControllers(option =>
            {
                option.SuppressAsyncSuffixInActionNames = false;
            });

            // Add HttpClient
            services.AddHttpClient();

            // add and config swaggerUI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DK_API",
                    Version = "v1",
                    Description = "Api_DK thông tin thực thể ở cuối ",
                    Contact = new OpenApiContact
                    {
                        Name = "DICH VU DANG KIEM",
                        Email = string.Empty,
                        Url = new Uri("https://1.dangkiem.online"),
                    }

                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            
            // add CORS policy
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder => 
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                );
            });

            // config Auto Mapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseHangfireDashboard();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DK_API v1");
                c.DocumentTitle = "Swagger-ui API Đăng kiểm";
            });

            app.UseCors();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Để sử dụng chính xác Kestrel với nginx, thêm vào đầu Startup.configure. Cấu hình X-Forwarded-For và  X-Forwarded-Proto 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
