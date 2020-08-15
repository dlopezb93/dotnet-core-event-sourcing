using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using User.Application.Services;
using User.Domain.AggregatesModels.UserAgg;
using User.Domain.AggregatesModels.UserAgg.Events;
using User.Domain.AggregatesModels.UserAgg.Factories;
using User.Infrastructure.Data.Repository;

namespace UserAPI
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
            services.AddControllers();

            services.AddMediatR(typeof(UserCreatedEvent).Assembly);
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();

                var config = new AmazonDynamoDBConfig()
                {
                    ServiceURL = "http://localhost:8000",
                };
                var client = new AmazonDynamoDBClient(config);
                
                IDynamoDBContext context = new DynamoDBContext(client);

                return context;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
