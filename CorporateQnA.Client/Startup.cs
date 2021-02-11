using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityServer4;
using CorporateQnA.Client.Config;
using CorporateQnA.Client.Data;
using CorporateQnA.Services.Models;


namespace CorporateQnA.Client
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		private Container Container = new SimpleInjector.Container();

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews().AddRazorRuntimeCompilation();

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			//Simple Injector
			services.AddSimpleInjector(Container, options =>
			{
				options.AddAspNetCore()
					.AddControllerActivation();
			});

			//Register Dependencies
			SimpleInjectorBootStrap.InitializeContainer(Container, Configuration);

			services.AddAuthorization(options =>
			{
				options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
				{
					policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
					policy.RequireAuthenticatedUser();
				});
			});

			services.AddLocalApiAuthentication();

			services.AddIdentity<AppIdentityUser, IdentityRole>(options =>
			{
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.User.RequireUniqueEmail = true;
				options.Password.RequireDigit = false;
			})
			 .AddEntityFrameworkStores<AppDbContext>()
			 .AddDefaultTokenProviders();

			services
			  .AddIdentityServer()
			  .AddAspNetIdentity<AppIdentityUser>()
			  .AddInMemoryApiResources(IdentityServerConfig.GetApis())
			  .AddInMemoryClients(IdentityServerConfig.GetClients())
			  .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
			  .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
			  .AddDeveloperSigningCredential();

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnection"));
			});

			services.ConfigureApplicationCookie(config =>
			{
				config.Cookie.Name = "IdentityServer.Cookie";
				config.LoginPath = "/Auth/Login";
				config.LogoutPath = "/Auth/Logout";
			});

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSimpleInjector(Container);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

			app.UseCors();

			app.UseIdentityServer();

			app.UseAuthorization();

			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});

			Container.Verify();

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
