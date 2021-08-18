using Hydra.AuthorizationServer.Extensions;

namespace Hydra.AuthorizationServer;
public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Hydra.AuthorizationServer", Version = "v1" });
        });

        services.AddIdentityServer4();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
      
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hydra.AuthorizationServer v1"));

        app.UseRouting();

        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}
