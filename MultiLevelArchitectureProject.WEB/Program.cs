using Microsoft.EntityFrameworkCore;
using MultiLevelArchitectureProject.BLL.Interfaces;
using MultiLevelArchitectureProject.BLL.Services;
using MultiLevelArchitectureProject.DAL.EF;
using MultiLevelArchitectureProject.DAL.Interfaces;
using MultiLevelArchitectureProject.DAL.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddTransient<IApplicationService, ApplicationService>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=ShowUsers}/{id?}");

app.Run();