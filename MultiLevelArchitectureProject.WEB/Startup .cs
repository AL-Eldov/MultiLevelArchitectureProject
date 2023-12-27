using Microsoft.EntityFrameworkCore;
using MultiLevelArchitectureProject.BLL.Interfaces;
using MultiLevelArchitectureProject.BLL.Services;
using MultiLevelArchitectureProject.DAL.EF;
using MultiLevelArchitectureProject.DAL.Interfaces;
using MultiLevelArchitectureProject.DAL.Repositories;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }
    public IConfigurationRoot Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddTransient<IUnitOfWork, EFUnitOfWork>();
        services.AddTransient<IApplicationService, ApplicationService>();

        string connection = Configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(x => x.MapControllers());
    }
}


