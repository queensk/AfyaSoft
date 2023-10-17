using AfyaSoftAppointment.Data;
using AfyaSoftAppointment.Extensions;
using AfyaSoftAppointment.Services;
using AfyaSoftAppointment.Services.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    // build.WithOrigins("https://localhost:7203", "http://localhost:7203");
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));

builder.AddSwaggerGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "POST API");
        c.RoutePrefix = string.Empty;
    }
});

app.UseMigration();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("policy1");
app.MapControllers();

app.Run();