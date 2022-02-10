using Snow_simulation.Model.Physic;using SnowInBrowser.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
MainPhysic  _physic = new MainPhysic(500, 500);
Converter _converter = new Converter(_physic.GetSnowFlakes(), _physic.GetSnowDrift());
_physic.Simulate();
Task.Run(() => _converter.Serialize());
app.Run();