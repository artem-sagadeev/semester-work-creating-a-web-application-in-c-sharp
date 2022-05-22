using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using WebApp.Areas.Identity.Data;
using WebApp.Hubs;
using WebApp.Middlwares;
using WebApp.Services;
using WebApp.Services.Chats;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Payment;
using WebApp.Services.Posts;
using WebApp.Services.Subscription;

namespace WebApp
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
            services.AddRazorPages();
            services.AddSignalR();
            services.AddSession();

            services.AddHttpClient<IDeveloperService, DeveloperService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            services.AddHttpClient<IPostsService, PostsService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            services.AddHttpClient<IFileService, FileService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            
            services.AddHealthChecks()
                .AddUrlGroup(new Uri(Configuration["ApiSettings:GatewayAddress"]), "Gateway.API", HealthStatus.Degraded);

            services.AddHttpClient<IPaymentService, PaymentService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            services.AddHttpClient<IChatService, ChatService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(Configuration["Database:ConnectionString"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context)
        {
            if (Configuration.GetValue<bool>("AutoMigration"))
            {
                context.Database.Migrate();
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWriteOffMoneyMiddleware();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); 
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}