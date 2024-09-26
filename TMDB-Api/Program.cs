using TMDB_Api.Configurations;
using TMDB_Api.Interfaces;
using TMDB_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<ITMDBService, TMDBService>();
builder.Services.Configure<TMDBConfig>(builder.Configuration.GetSection("TMDB"));
builder.Services.AddSingleton<CachingService>();

//cors allow all

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors();
app.Run();
