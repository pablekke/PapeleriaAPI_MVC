using DataAccess;
using Services;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddScoped(typeof(IRepositorioLogin), provider => new RepositorioLogin(
                    builder.Configuration.GetConnectionString("UrlLogin")));

            services.AddScoped(typeof(IRepositorioEncargado), provider => new RepositorioEncargado(
                    builder.Configuration.GetConnectionString("UrlEncargado"), new HttpContextAccessor()));

            services.AddScoped(typeof(IRepositorioArticulo), provider => new RepositorioArticulo(
                    builder.Configuration.GetConnectionString("UrlArticulo"), new HttpContextAccessor()));

            services.AddScoped(typeof(IRepositorioMovimiento), provider => new RepositorioMovimiento(
                    builder.Configuration.GetConnectionString("UrlMovimiento"), new HttpContextAccessor()));

            services.AddScoped(typeof(IRepositorioTipoMovimiento), provider => new RepositorioTipoMovimiento(
                    builder.Configuration.GetConnectionString("UrlTipoMovimiento"), new HttpContextAccessor()));

            services.AddScoped(typeof(IServicioLogin), typeof(ServicioLogin));
            services.AddScoped(typeof(IServicioEncargado), typeof(ServicioEncargado));
            services.AddScoped(typeof(IServicioArticulo), typeof(ServicioArticulo));
            services.AddScoped(typeof(IServicioMovimiento), typeof(ServicioMovimiento));
            services.AddScoped(typeof(IServicioTipoMovimiento), typeof(ServicioTipoMovimiento));

            // Adición IHttpContextAccessor al contenedor de servicios
            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            services.AddControllersWithViews();
            services.AddHttpClient();

            #region Sesión
            services.AddDistributedMemoryCache(); // Usar memoria como back store de caché

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300); // Tiempo de expiración de la sesión
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            #endregion

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Encargado}/{action=Encargados}/{id?}");

            app.Run();
        }
    }
}