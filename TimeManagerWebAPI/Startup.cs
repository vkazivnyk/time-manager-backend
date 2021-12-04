using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TimeManageData.DbContexts;
using TimeManageData.Models;
using TimeManageData.Repositories;
using TimeManagerServices.Auth;
using TimeManagerWebAPI.GraphQL;
using TimeManagerWebAPI.GraphQL.ErrorFilters;
using TimeManagerWebAPI.GraphQL.Tasks;
using TimeManagerWebAPI.GraphQL.Users;

namespace TimeManagerWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string corsOrigin = Environment.IsDevelopment()
                ? @"http://localhost:3000"
                : null;

            services.AddCors(options =>
            {
                options.AddPolicy("Default",
                    builder => builder
                        .WithOrigins(corsOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddPooledDbContextFactory<TimeManagerDbContext>(options =>
            {
                string connectionString = Environment.IsDevelopment()
                    ? Configuration["LocalConnection"]
                    : Configuration["RemoteConnection"];

                options.UseSqlServer(connectionString);
            });

            services.AddScoped(p => p.GetRequiredService<IDbContextFactory<TimeManagerDbContext>>().CreateDbContext());

            services.AddSingleton<IRepository<UserTask>, UserTaskMockRepository>();

            services.AddSingleton<IRepository<ApplicationUser>, UserMockRepository>();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<UserTaskType>()
                .AddType<UserTaskPayloadType>()
                .AddType<UserTaskAddInputType>()
                .AddType<UserTaskAddPayloadType>()
                .AddType<UserTaskPutInputType>()
                .AddType<UserTaskPutPayloadType>()
                .AddType<UserTaskDeleteInputType>()
                .AddType<UserTaskDeletePayloadType>()
                .AddType<ApplicationUserType>()
                .AddFiltering()
                .AddSorting()
                .AddAuthorization();

            services.AddErrorFilter<GraphQLErrorFilter>();

            services.AddControllers();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<TimeManagerDbContext>();

            string signingKeyPhrase = Configuration["SigningKeyPhrase"];
            SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(signingKeyPhrase));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = true;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddScoped<JwtTokenCreator>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Auth", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "Auth"));
            });

            services.AddHttpContextAccessor();

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

            app.UseCors("Default");

            app.UseRouting();

            app.UseIpRateLimiting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
            });
        }
    }
}
