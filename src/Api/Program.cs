using RbbSolucoes.Website.Backend.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddApiConfiguration();
services.AddBasicAuthentication();
//services.AddLoggingConfiguration();
services.AddDependencyResolver();
services.AddDatabaseConfiguration();
builder.AddAppSettingsConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapControllers();
}

app.UseHttpsRedirection();

app.UseBasicAuthentication();

app.UseApiConfiguration();

app.UseCors("AllowAll");

app.UseMigration();

app.Run();