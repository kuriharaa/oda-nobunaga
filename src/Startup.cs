using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GiveawayFreeSteamBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                            optional: false,
                            reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IGiveawayService, GiveawayService>();
            services.AddScoped<IFeedConnector, FeedConnector>();
            services.AddScoped<IFeedProvider, FeedProvider>();
            services.AddScoped<IFeedParser, FeedParser>();
            services.AddScoped<IDiscordService, DiscordService>();
            services.AddScoped<IGiveawayRepository, GiveawayRepository>();
            //services.Configure<MongoConfig>(options =>
            //{
            //    options.connectionString = Configuration["mongo:connectionString"];
            //    options.dbName = Configuration["mongo:dbName"];
            //    options.collectionName = Configuration["mongo:collectionName"];
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
