using DemoApp.Api;
using DemoApp.Application;
using DemoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var configBuilder = new ConfigurationBuilder()
       .SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", reloadOnChange: false, optional: true)
       .AddEnvironmentVariables();

    IConfiguration config = configBuilder.Build();

    builder.Services
        .AddApplication()
        .AddInfrastructure(config)
        .AddPresentation();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

