using RbbSolucoes.Website.Backend.Api.Configurations;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
    
var builder = WebApplication.CreateBuilder(args);
builder.AddAppSettingsConfiguration();

builder.Host.UseSerilog();

var services = builder.Services;
services.AddApiConfiguration();
services.AddBasicAuthentication();
//services.AddLoggingConfiguration();
services.AddDependencyResolver();
services.AddDatabaseConfiguration();

var app = builder.Build();

app.UseHttpsRedirection();    // 1. HTTPS Redirect
app.UseRouting();             // 2. Routing
app.UseBasicAuthentication(); // 3. ✅ Autenticação Básica
app.UseApiConfiguration();    // 4. Swagger, Rate Limiter, etc.
app.UseCors("AllowAll");      // 5. CORS
app.UseAuthorization();       // 6. Authorization
app.MapControllers();         // 7. ✅ Mapeamento de Controllers
app.UseMigration();
app.Run();
