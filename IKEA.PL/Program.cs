using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Identity;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services 
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            //builder.Services.AddScoped<IDepartment,DepartmentRepo>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication().AddCookie(option =>
            {
                option.LoginPath = "/Account/LogIn";
                option.AccessDeniedPath = "/Home/Error";
                option.ExpireTimeSpan = TimeSpan.FromDays(2);
                option.ForwardSignOut = "/Account/LoIn";
            });

            builder.Services.AddScoped<IUOW,UOW>();

            builder.Services.AddScoped<IDepartmentService ,DepartmentService>();


            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();


            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));

            //this part not used anymore
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>(sevice =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionsBuilder.UseSqlServer("Server=.;Database=IKEA;User Id=sa;password=0100;Integrated Security=false;");

            //    var options=optionsBuilder.Options;
            //    return options;
            //});
            #endregion

            var app = builder.Build();

            #region MiddleWares
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion


            app.Run();
        }
    }
}
