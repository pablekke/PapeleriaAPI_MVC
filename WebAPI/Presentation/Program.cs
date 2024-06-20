using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Services;
using DataAccess;
using System.Text;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Conexiones
            builder.Services.AddDbContext<DbContext, Contexto>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnection"));
            });
            #endregion

            // Dependencias
            builder.Services.AddScoped<IServicioLogin, ServicioLogin>();

            builder.Services.AddScoped<IServicioEncargado, ServicioEncargado>();
            builder.Services.AddScoped<IRepositorioEncargado, RepositorioEncargado>();

            builder.Services.AddScoped<IServicioTipoMovimiento, ServicioTipoMovimiento>();
            builder.Services.AddScoped<IRepositorioTipoMovimiento, RepositorioTipoMovimiento>();

            builder.Services.AddScoped<IServicioArticulo, ServicioArticulo>();
            builder.Services.AddScoped<IRepositorioArticulo, RepositorioArticulo>();

            builder.Services.AddScoped<IServicioStockArticulo, ServicioStockArticulo>();
            builder.Services.AddScoped<IRepositorioStockArticulo, RepositorioStockArticulo>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region JWT Configuration
            // Leer la clave secreta desde appsettings.json
            var claveSecreta = builder.Configuration["Jwt:Clave"];
            var claveBytes = Encoding.ASCII.GetBytes(claveSecreta);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(claveBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Configurar la autorizaci�n
            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}