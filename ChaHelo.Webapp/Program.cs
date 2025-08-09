using ChaHelo.Application.Usecases;
using ChaHelo.Infra.Context;
using ChaHelo.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ChaHeloDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("lite"));
});
builder.Services.AddScoped<PresenceRepository>();
builder.Services.AddScoped<ConfirmPresenceUseCase>();
builder.Services.AddScoped<ListPresencesUseCase>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Presence}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ChaHeloDbContext>();
    db.Database.Migrate();
}


app.Run();
