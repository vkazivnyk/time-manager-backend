using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeManageData.DbContexts;
using TimeManagerWebAPI.GraphQL;
using TimeManagerWebAPI.GraphQL.ErrorFilters;
using TimeManagerWebAPI.GraphQL.Tasks;
using TimeManagerWebAPI.GraphQL.Users;

namespace TimeManagerWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<TimeManagerDbContext>(options =>
            {
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=time-manager;Trusted_Connection=True;");
            });

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<UserTaskType>()
                .AddType<UserTaskPayloadType>()
                .AddType<UserTaskPutInputType>()
                .AddType<UserTaskPutPayloadType>()
                .AddType<ApplicationUserType>();

            services.AddErrorFilter<GraphQLErrorFilter>();

            services.AddMemoryCache();

            services.AddInMemoryRateLimiting();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseGraphQLVoyager(new VoyagerOptions
                {
                    GraphQLEndPoint = "/graphql"
                }, "/graphql-voyager");
            }

            app.UseRouting();

            app.UseIpRateLimiting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
