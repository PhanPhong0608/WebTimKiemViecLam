using DACS_WebTimKiemViecLam.Models;
using DACS_WebTimKiemViecLam.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JobDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

// Add services to the container.
builder.Services.AddControllersWithViews();

// ??ng ký các Repository
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<ICompanyRepository, EFCompanyRepository>();
builder.Services.AddScoped<IFieldRepository, EFFieldRepository>();
builder.Services.AddScoped<IJobPositionRepository, EFJobPositionRepository>();
builder.Services.AddScoped<IJobApplicationRepository, EFJobApplicationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
