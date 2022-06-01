using Day3.data;
using Day3.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<IStudent,StudentMoc>();
builder.Services.AddScoped<IStudent,StudentDb>();
builder.Services.AddScoped<IDepartment, DepartmentDB>();

builder.Services.AddDbContext<ITIDBContext>(a =>
{
    a.UseSqlServer("Server=.;Database=mvcITI;Trusted_Connection=True;");
});

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
    pattern: "{controller=departmentCourses}/{action=Index}/{id?}");

app.Run();
