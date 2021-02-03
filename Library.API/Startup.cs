using AutoMapper;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Repository;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;

namespace Library.API
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
            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
                config.Filters.Add<JsonExceptionFilter>();
                config.CacheProfiles.Add("Default", new CacheProfile { Duration = 60 });
            }).AddNewtonsoftJson(setup =>
            setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()).AddXmlDataContractSerializerFormatters();
            services.AddDbContext<LibraryDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("LiBraryAPIDbConnection"));
            });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.API", Version = "v1" });
            });
            services.AddScoped<CheckAuthorExistFilterAttribute>();
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            }); //添加服务器响应缓存
            services.AddMemoryCache(); //添加内存缓存
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["Caching:Host"];
                options.InstanceName = Configuration["Caching:Instance"];
            }); //添加分布式Redis缓存
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; //配置客户端未指定版本时是否只用默认值
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true; //配置响应中是否展示支持和不支持的版本列表
                //options.ApiVersionReader = new QueryStringApiVersionReader("ver"); //自定义api版本查询参数名称
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version"); // 添加api-version消息头
                //options.ApiVersionReader = new MediaTypeApiVersionReader(); //添加Accept或Content-Type指定Api版本
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader(),
                    new QueryStringApiVersionReader("api-version")
                    );
            });
            services.AddGraphQLSchemaAndTypes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.API v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}