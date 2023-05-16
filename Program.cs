using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("RazorPagesMovieContext")));
    // detect and diagnose errors with Entity Framework Core migrations.
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}
else
{
    builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMovieContext")));
}

var app = builder.Build();

// seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // each time it will Delete the database. Change the data model. 
    // Run the app. EnsureCreated creates a database with the new schema.
    // var context = services.GetRequiredService<RazorPagesMovieContext>();
    // context.Database.EnsureCreated();
    SeedData.Initialize(services);

    // to use migrations 
    // run dotnet ef database drop
    // dotnet ef migrations add migrationName
    // dotnet ef database update
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
