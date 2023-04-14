using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Modules;
using TallerMecanicoDiWork.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(x => x.AddPolicy("EnableCors", builder =>
{
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            //.WithOrigins("https://codestack.com")
            .AllowAnyMethod()
            //.WithMethods("PATCH", "DELETE", "GET", "HEADER")
            .AllowAnyHeader();
    //.WithHeaders("X-Token", "content-type")
}));

//Repositories
builder.Services.AddScoped<IVehiculoRepository,VehiculoRepository>();
builder.Services.AddScoped<IRepuestoRepository, RepuestoRepository>();
builder.Services.AddScoped<IDesperfectoRepository, DesperfectoRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();

//Modules
builder.Services.AddScoped<IVehiculoModule,VehiculoModule>();
builder.Services.AddScoped<IRepuestoModule, RepuestoModule>();
builder.Services.AddScoped<IDesperfectoModule, DesperfectoModule>();
builder.Services.AddScoped<IPresupuestoModule, PresupuestoModule>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("EnableCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
