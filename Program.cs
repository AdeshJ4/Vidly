using Microsoft.EntityFrameworkCore;
using Vidly.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*
//custom route
app.MapControllerRoute(
    name: "MoviesByReleaseDate",
    pattern: "movies/released/{year?}/{month?}",
    defaults: new { controller = "Movies", action = "ByReleaseDate" },
    //new { year = @"\d{4}", month = @"\d{2}"}); // d -> digit (you must specify 4 digit year and 2 digit month otherwise tyou will get error)
    new { year = @"2015|2016", month = @"\d{2}" }); // only 2015 & 2016 is enable. 
*/


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
