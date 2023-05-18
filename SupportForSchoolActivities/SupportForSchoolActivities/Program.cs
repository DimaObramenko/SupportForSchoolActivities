using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupportForSchoolActivities.DAL;
using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.DAL.Repository;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service;
using SupportForSchoolActivities.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//�������� ����� ���������� � ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//������ �������� ApplicationDbContext � ����� ������ � ������� 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
