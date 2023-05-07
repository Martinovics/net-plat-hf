using Microsoft.EntityFrameworkCore;
using NetPlatHF.BLL.Dtos;
using NetPlatHF.DAL;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    // options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"])  // így is lehet
);


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));  // a dto-hoz kell


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
