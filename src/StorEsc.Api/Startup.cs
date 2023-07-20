using MediatR;
using StorEsc.API.IoC;
using StorEsc.IoC.Dependencies.ApplicationServices;
using StorEsc.IoC.Dependencies.Database;
using StorEsc.IoC.Dependencies.DomainServices;
using StorEsc.IoC.Dependencies.ExternalServices;
using StorEsc.IoC.Dependencies.Hashers;
using StorEsc.IoC.Dependencies.Mapper;
using StorEsc.IoC.Dependencies.Mediator;
using StorEsc.IoC.Dependencies.Repositories;

namespace StorEsc.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IWebHostEnvironment environment)
    {
        _environment = environment;

        var builder = new ConfigurationBuilder()
            .SetBasePath(_environment.ContentRootPath)
            .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        if(_environment.EnvironmentName == "Local")
            builder.AddUserSecrets<Startup>();

        _configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddScoped((_) => _configuration)
            .AddDatabaseContext(_configuration)
            .AddRepositories()
            .AddDomainServices()
            .AddApplicationServices()
            .AddExternalServices()
            .AddArgon2Id(_configuration)
            .AddMediatR(typeof(Startup))
            .AddMediator()
            .AddMapper()
            .AdDtokenAuthentication(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_environment.EnvironmentName == "Local")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        
        app.UseStaticFiles();
        
        app.UseAuthentication();
            
        app.UseRouting();

        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}