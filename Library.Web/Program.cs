using Blazored.LocalStorage;
using Library.Web.Config;
using Library.Web.Providers;
using Library.Web.Services;
using Library.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri(builder.Configuration["BaseAddress"]));
builder.Services.AddAntDesign();

builder.Services.AddAutoMapper(typeof(MapperConfig));

#region 注册service

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILendConfigService, LendConfigService>();
builder.Services.AddScoped<ILendRecordService, LendRecordService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoticeService, NoticeService>();
builder.Services.AddScoped<IDashBoardService, DashBoardService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

#endregion

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<ApiAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();