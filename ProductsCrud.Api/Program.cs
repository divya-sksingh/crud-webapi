using Microsoft.EntityFrameworkCore;
using ProductsCrud.Api;
using System.Reflection;


WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
var environment = webApplicationBuilder.Environment.EnvironmentName;

ConfigurationBuilder configurationBuilder = new();
IConfigurationBuilder builder = configurationBuilder.AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();


// Add services to the container.

webApplicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
webApplicationBuilder.Services.AddEndpointsApiExplorer();
webApplicationBuilder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});
webApplicationBuilder.Services.AddScoped<IProductRepository, ProductRepository>();
webApplicationBuilder.Services.AddTransient<ProductDbContext>();
webApplicationBuilder.Services.AddDbContext<ProductDbContext>(options =>
       options.UseSqlServer(configuration.GetValue<string>("DefaultConnection")));

var app = webApplicationBuilder.Build();

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
