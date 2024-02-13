using Microsoft.EntityFrameworkCore;

using MovieBooking.DataContext.Data;
using MovieBooking.Services.BookingServices;
using MovieBooking.Services.MovieServices;
using MovieBooking.Services.TimingsServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your DbContext and specify the connection string
builder.Services.AddScoped<DataContexts>();


builder.Services.AddDbContext<DataContexts>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



// Register your movie services
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IMovieTimingsServices, MovieTimingsServices>();
builder.Services.AddScoped<IShowBookingServices, ShowBookingServices>();
var app = builder.Build();


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
