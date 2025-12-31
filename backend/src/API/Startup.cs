using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configura os serviços (equivalente a builder.Services)
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Swagger + JWT
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Informe o token JWT no formato: Bearer {seu_token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Database
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]!)
                        )
                    };
                });

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Dependency Injection
            services.AddScoped<IPatternRepository, PatternRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<
                Microsoft.AspNetCore.Identity.IPasswordHasher<Domain.Entities.User>,
                Microsoft.AspNetCore.Identity.PasswordHasher<Domain.Entities.User>
            >();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        // Configura o pipeline HTTP (equivalente ao builder.Build())
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Criar database se não existir
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }

    }
}
