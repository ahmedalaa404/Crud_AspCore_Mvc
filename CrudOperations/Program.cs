using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BLL_CrudOperations.InterFaces;
using BLL_CrudOperations.Repos;
using CrudOperations.Profiler;
using Dal_CrudOperations.Database;
using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using CrudOperations.ConfigAppSetting;
using CrudOperations.Helper;
using System;

namespace CrudOperations
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var Builder = WebApplication.CreateBuilder(args);


			#region DependencyInjections
			// This method gets called by the runtime. Use this method to add Builder.Builder.Services to the container.

			Builder.Services.AddControllersWithViews();

			Builder.Services.AddDbContext<Context>(options =>
			{
				options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnections"));
			});




			//Builder.Services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();

			//Builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
			Builder.Services.AddScoped<IUniteOFWork, UniteOfWork>();


			Builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfilers()));

			// Configuration of Account of Security Module 

			Builder.Services.AddIdentity<IdentityUser, IdentityRole>()
			.AddEntityFrameworkStores<Context>()   // To Active All Interfaces of Identity
			.AddDefaultTokenProviders(); //Used To Generate Token Provieder
			Builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
					x =>
					{
						x.LoginPath = "account/Login";
						x.AccessDeniedPath = "Home/Error";
						x.ExpireTimeSpan = TimeSpan.Parse("20");

						
					}
				
				
				);















			Builder.Services.Configure<mailSetting>(Builder.Configuration.GetSection("mailSetting"));
			Builder.Services.AddTransient<ImailSettings,EmailSettings>();

			#region Auth Builder.Services
			//Builder.Services.AddScoped<UserManager<ApplicationsUser>>();
			//Builder.Services.AddScoped<SignInManager<ApplicationsUser>>();
			//Builder.Services.AddScoped<RoleManager<IdentityRole>>();
			#endregion







			#endregion



			#region Config Http
			var app = Builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();


			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=home}/{action=index}/{id?}");
			});

			app.Run();

			#endregion
		}


	}
}
