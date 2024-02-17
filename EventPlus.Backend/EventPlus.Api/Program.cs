using EventPlus.Api;
using EventPlus.Api.Middlewares;
using EventPlus.Application;
using EventPlus.Domain;
using EventPlus.Infrastructure;
using NeerCore.Api.Extensions;
using NeerCore.Api.Swagger.Extensions;
using NeerCore.Logging;
using NeerCore.Logging.Extensions;

var logger = LoggerInstaller.InitFromCurrentEnvironment();

try
{
    var builder = WebApplication.CreateBuilder(args);
    logger.Debug("Configuring application builder");
    ConfigureBuilder(builder);

    var app = builder.Build();
    logger.Info("Syncing database migrations");
    MigrateDatabase(app);

    logger.Debug("Configuring web application");
    // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    ConfigureWebApp(app);
    
    app.Run();
}
catch (Exception e)
{
    logger.Fatal(e);
}
finally
{
    logger.Info("Application is now stopping");
}

static void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Logging.ConfigureNLogAsDefault();
    builder.Configuration.AddJsonFile("appsettings.Local.json");
    builder.Configuration.AddJsonFile("appsettings.Development.json");

    // Api
    builder.Services.AddApi(builder.Configuration, builder.Environment);
    // Database
    builder.Services.AddDatabase(builder.Configuration);
    // Application & Infrastructure
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(o => { o.UseAllOfToExtendReferenceSchemas(); });
}

static void ConfigureWebApp(WebApplication app)
{
    app.UseDeveloperExceptionPage();

    if (app.Configuration.GetSwaggerSettings().Enabled)
    {
        app.UseNeerSwagger();
        app.ForceRedirect(from: "/", to: "/swagger");
    }

    app.UseMiddleware<ExceptionsHandlerMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

static void MigrateDatabase(WebApplication app)
{
    // using var scope = app.Services.CreateScope();
    // if (scope.ServiceProvider.GetRequiredService<ISqlServerDatabase>() is not SqlServerDbContext database)
    // throw new InternalServerException($"{nameof(ISqlServerDatabase)} DB context cannot be resolved");
    // database.Database.Migrate();
}