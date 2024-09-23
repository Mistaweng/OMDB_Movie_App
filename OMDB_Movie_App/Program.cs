using OMDB_Movie_App.Configurations;
using OMDB_Movie_App.Helpers;
using OMDB_Movie_App.Repositories.Interfaces;
using OMDB_Movie_App.Repositories;
using OMDB_Movie_App.Services.Interfaces;
using OMDB_Movie_App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<OmdbSettings>(builder.Configuration.GetSection("OmdbSettings"));
builder.Services.AddSingleton<HttpClientHelper>();

builder.Services.AddScoped<IMovieRepository, OmdbRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
	options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
