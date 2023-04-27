using Microsoft.EntityFrameworkCore;
using NetPlatHF.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.Configure<DbSecrets>(builder.Configuration.GetSection(nameof(DbSecrets)));


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    // options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"])  // így is lehet
);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
