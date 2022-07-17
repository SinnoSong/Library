using Library.API.Configs;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Service;
using Library.API.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 获取appsettings中配置的db链接字符串
var connString = builder.Configuration.GetConnectionString("LibraryAPIDbConnection");
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connString));

// 添加autoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// 添加swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "输入token，格式为Bearer xxx(中间必须有空格)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// 添加identityCore，基于user和role
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<LibraryDbContext>();
// 添加repository包装
builder.Services.AddScoped<IServicesWrapper, ServicesWrapper>();
// 添加定时任务
builder.Services.AddHostedService<BookStatusService>();
builder.Services.AddControllers(config =>
{
    config.ReturnHttpNotAcceptable = true;
    config.Filters.Add<JsonExceptionFilter>();
});

// 添加Serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
//添加内存缓存
builder.Services.AddMemoryCache();
//添加服务器响应缓存
builder.Services.AddResponseCaching(options =>
{
    options.UseCaseSensitivePaths = true;
    options.MaximumBodySize = 1024;
});

//添加分布式Redis缓存
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration["Caching:Host"];
    options.InstanceName = builder.Configuration["Caching:Instance"];
});
// 添加Api版本配置
builder.Services.AddApiVersioning(options =>
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
// 添加认证
var tokenSection = builder.Configuration.GetSection("Security:Token");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenSection["Issuer"],
        ValidAudience = tokenSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSection["Key"])),
        ClockSkew = TimeSpan.Zero
    };
}); //添加基于JwtToken的认证
// 添加跨域请求
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMethodsPolicy", builder => builder.WithOrigins("https://localhost:5001").AllowAnyMethod());
    options.AddPolicy("AllowAnyOriginPolicy", builder => builder.AllowAnyOrigin());
    options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost:5001"));
});
// 添加支持名称带async的方法名称
builder.Services.AddMvc(options => { options.SuppressAsyncSuffixInActionNames = false; });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseCors(builder => builder.WithOrigins("https://localhost:5001"));
app.UseCors("AllowAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();