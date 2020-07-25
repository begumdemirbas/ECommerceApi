using Autofac;
using Autofac.Features.AttributeFilters;
using ECommerceApi.Application.Queries;
using ECommerceApi.Data;
using ECommerceApi.Data.Data.Core;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate.Services;
using ECommerceApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "UserCookie";
                    options.LoginPath = "/Account/Login/";
                });
            services.AddControllersWithViews();

            services.AddDbContext<Context>(
                           options => options
                                .UseSqlServer(Configuration.GetConnectionString(name: "DbConnection")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
               .As<IUnitOfWork>()
               .Keyed<IUnitOfWork>("Context")
               .WithAttributeFiltering()
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .WithAttributeFiltering()
                .InstancePerDependency();

            builder.RegisterType<UserQuery>()
                .As<IUserQuery>()
                .WithAttributeFiltering()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductQuery>()
                .As<IProductQuery>()
                .WithAttributeFiltering()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .WithAttributeFiltering()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
