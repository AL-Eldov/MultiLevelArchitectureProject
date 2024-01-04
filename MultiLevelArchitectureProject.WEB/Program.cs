var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=ShowUsers}/{id?}");

app.Run();