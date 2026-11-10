var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.MapControllerRoute("default", "{controller=Base36}/{action=Index}/{id?}");
app.Run();
