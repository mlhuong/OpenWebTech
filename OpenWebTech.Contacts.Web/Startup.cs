using HMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OpenWebTech.Contacts.DataModels;
using OpenWebTech.Contacts.Repositories;
using OpenWebTech.Contacts.Services;
using OpenWebTech.Contacts.Services.Mappings;
using OpenWebTech.Contacts.Web;
using OpenWebTech.Contacts.Web.Filters;
using System;

namespace OpenWebTech.Contacts
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
            services.AddDbContext<ContactsDbContext>(opt =>
               opt.UseInMemoryDatabase("ContactsDb"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ContactsDbContext>();

            // secretKey contains a secret passphrase only your server knows
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AccountService.SigningKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "OpenWebTech_Issuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "OpenWebTech_Audience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            authenticationBuilder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                //c.OperationFilter<SwaggerHeaderParameter>();
                
                // Bearer token authentication
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
                c.AddSecurityRequirement(securityRequirements);
            });

            services.AddScoped<AccountService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ContactService>();
            services.AddScoped<SkillService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error"); // Add this

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // Mappings
            MapConfig.Initialize(initializer =>
            {
                ServicesMapperConfiguration.Init(initializer);
            });
        }
    }
}
