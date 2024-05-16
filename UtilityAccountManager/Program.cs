using UtilityAccountManager.Data;
using Microsoft.EntityFrameworkCore;
using UtilityAccountManager.Services.Interfaces;
using UtilityAccountManager.Services;
using UtilityAccountManager.Repository.Interfaces;
using UtilityAccountManager.Repository;
using System.Text.Json.Serialization;
using Mapster;
using MapsterMapper;
using System.Reflection;
using UtilityAccountManager.MappingConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.MaxDepth = 4;
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UtilityAccountContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("UtilityAccountConnSQLite")));

builder.Services.AddTransient<IUtilityAccountService, UtilityAccountService>();

builder.Services.AddScoped<IUtilityAccountRepository, UtilityAccountRepository>();

builder.Services.AddSingleton<IRegister, ResidentUtilityAccountMappingConfiguration>();
builder.Services.AddSingleton<IMapper>(servicesProvider =>
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        var iRegisters = servicesProvider.GetServices<IRegister>();
        typeAdapterConfig.Apply(iRegisters);
        return new Mapper(typeAdapterConfig);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedData.EnsurePopulated(app);

app.Run();


//builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookRepository", Version = "v1" }); });

//builder.Services.AddApiVersioning(options => { options.DefaultApiVersion = new ApiVersion(1, 1); options.ReportApiVersions = true; });

//builder.Services.AddRazorPages();

