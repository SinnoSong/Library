using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Library.API.Entities;
using Library.API.Repository.Interface;
using Library.API.Repository;
using Library.API.Configs;
using Library.API.Helper;
using Library.API.Configs.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ��ȡappsettings�����õ�db�����ַ���
var connString = builder.Configuration.GetConnectionString("LiBraryAPIDbConnection");
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connString));

// ���autoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// ���swagger
builder.Services.AddEndpointsApiExplorer();

// ���identityCore������user��role
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<LibraryDbContext>();

// ���repository��װ
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddSingleton<HashFactory>();
builder.Services.AddControllers(config =>
{
    config.ReturnHttpNotAcceptable = true;
    config.Filters.Add<JsonExceptionFilter>();
    config.CacheProfiles.Add("Default", new CacheProfile { Duration = 60 });
}).AddNewtonsoftJson(setup =>
setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()).AddXmlDataContractSerializerFormatters();

// ���Serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
//����ڴ滺��
builder.Services.AddMemoryCache();
builder.Services.AddScoped<CheckAuthorExistFilterAttribute>();
//��ӷ�������Ӧ����
builder.Services.AddResponseCaching(options =>
{
    options.UseCaseSensitivePaths = true;
    options.MaximumBodySize = 1024;
});

//��ӷֲ�ʽRedis����
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration["Caching:Host"];
    options.InstanceName = builder.Configuration["Caching:Instance"];
});
// ���Api�汾����
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; //���ÿͻ���δָ���汾ʱ�Ƿ�ֻ��Ĭ��ֵ
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true; //������Ӧ���Ƿ�չʾ֧�ֺͲ�֧�ֵİ汾�б�
                                      //options.ApiVersionReader = new QueryStringApiVersionReader("ver"); //�Զ���api�汾��ѯ��������
                                      //options.ApiVersionReader = new HeaderApiVersionReader("api-version"); // ���api-version��Ϣͷ
                                      //options.ApiVersionReader = new MediaTypeApiVersionReader(); //���Accept��Content-Typeָ��Api�汾
    options.ApiVersionReader = ApiVersionReader.Combine(
        new MediaTypeApiVersionReader(),
        new QueryStringApiVersionReader("api-version")
        );
});
// �����֤
var tokenSection = builder.Configuration.GetSection("Security:Token");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
}); //��ӻ���JwtToken����֤
// ��ӿ�������
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMethodsPolicy", builder => builder.WithOrigins("https://localhost:5001").AllowAnyMethod());
    options.AddPolicy("AllowAnyOriginPolicy", builder => builder.AllowAnyOrigin());
    options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost:5001"));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.API v1"));
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseCors(builder => builder.WithOrigins("https://localhost:5001"));
app.UseCors("AllowAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.UseResponseCaching();

app.MapControllers();

app.Run();