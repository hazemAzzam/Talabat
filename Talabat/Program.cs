using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region ConfigureServices
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<StoreDBContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });
#endregion

var app = builder.Build();

using var scope = app.Services.CreateScope();

var servicesProvider = scope.ServiceProvider;

var _context = servicesProvider.GetRequiredService<StoreDBContext>();
var _logger = servicesProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

try
{
    await _context.Database.MigrateAsync();
    await StoreDBContextSeed.SeedAsync(_context);
}
catch (Exception ex)
{
    _logger.LogError(ex, "An Error Occured while Applying the Migrations");
}

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
