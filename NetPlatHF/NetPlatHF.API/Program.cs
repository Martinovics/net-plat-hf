using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using NetPlatHF.API.Authentication;
using NetPlatHF.API.Options;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.DAL.Data;
using NetPlatHF.BLL.Interfaces;
using NetPlatHF.BLL.Services;
using NetPlatHF.BLL.Classes;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            opt => opt
                .MigrationsAssembly("NetPlatHF.DAL")
                .EnableRetryOnFailure(
                    maxRetryCount: 4,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: Array.Empty<int>()  // alapertelmezett tranziens hibak 
                )
        )
);
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddTransient<IExerciseTemplateService, ExerciseTemplateService>();
builder.Services.AddTransient<IGroupTemplateService, GroupTemplateService>();
builder.Services.AddTransient<ITemplateService, TemplateService>();
builder.Services.AddTransient<IWorkoutService, WorkoutService>();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>()
    .AddCheck<DbHealthCheck>("Db Health Check");


var app = builder.Build();

app.MapHealthChecks("/health");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>  // hozz� kell adni a swagger endpointokat
    {
        var provider = app.Services.GetService<IApiVersionDescriptionProvider>()!;
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"{description.GroupName}/swagger.json",
                description.ApiVersion.ToString()
                );
        }
    });
}

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles();
app.Run();
