using BloodDonar.MVC.Data;
using BloodDonar.MVC.Services.Interfaces;
using BloodDonar.MVC.Services.Implementations;
using BloodDonar.MVC.Repusitories.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<BloodDonorDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCon")));
builder.Services.AddScoped<IBloodDonorRepository, BloodDonorRepository>();
//builder.Services.AddScoped<IBloodDonorService, BloodDonorService>(); // Where is the problem here?
builder.Services.AddScoped<BloodDonar.MVC.Services.Interfaces.IBloodDonorService, BloodDonar.MVC.Services.Implementations.BloodDonorService>();
builder.Services.AddTransient<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BloodDonor}/{action=Index}/{id?}");

app.Run();
