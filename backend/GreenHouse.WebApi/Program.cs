using GreenHouse.Data.EntityFramework;
using GreenHouse.Data.EntityFramework.Reposirories;
using GreenHouse.Domain.Interfaces;
using GreenHouse.Domain.Services;
using GreenHouse.WebApi.Configurations;
using GreenHouse.WebApi.Services;
using IdentityPasswordHasherLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

namespace GreenHouse.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();

            JwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JwtConfig").Get<JwtConfig>()!;
            if (jwtConfig is null) { throw new InvalidOperationException("JwtConfig is not configured"); }
            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddSingleton<ITokenService, TokenService>();

            string connection = builder.Configuration.GetConnectionString("DB_40CA8")!;
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddScoped<ICityRepository, CityRepositoryEf>();
            builder.Services.AddScoped<IAppartmentRepository, AppartmentRepositoryEf>();
            builder.Services.AddScoped<IAdminRepository, AdminRepositoryEf>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<AdminService>();
            builder.Services.AddSingleton<IApplicationPasswordHasher, IdentityPasswordHasher>();

            builder.Services.AddHttpLogging(options => //настройка
            {
                options.LoggingFields = HttpLoggingFields.RequestHeaders
                                        | HttpLoggingFields.ResponseHeaders
                                        | HttpLoggingFields.RequestBody
                                        | HttpLoggingFields.ResponseBody;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudiences = new[] { jwtConfig.Audience },
                    ValidIssuer = jwtConfig.Issuer
                };
            });
            builder.Services.AddAuthorization();

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images")),

                RequestPath = new PathString("/Images")
            });
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}