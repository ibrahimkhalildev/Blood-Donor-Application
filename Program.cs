using BloodDonar.MVC.Data;
using BloodDonar.MVC.Data.UnitOfWork;
using BloodDonar.MVC.Mapping;
using BloodDonar.MVC.Repusitories.Implementations;
using BloodDonar.MVC.Repusitories.Interfaces;
using BloodDonar.MVC.Services.Implementations;
using BloodDonar.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<BloodDonorDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCon")));
builder.Services.AddScoped<IBloodDonorRepository, BloodDonorRepository>();
//builder.Services.AddScoped<IBloodDonorService, BloodDonorService>(); // Where is the problem here?
builder.Services.AddScoped<BloodDonar.MVC.Services.Interfaces.IBloodDonorService, BloodDonar.MVC.Services.Implementations.BloodDonorService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDonationRepository, BloodDonar.MVC.Repusitories.Implementations.DonationRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile)); 



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
