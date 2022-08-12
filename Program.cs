using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using razorIdentityDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options
 .UseNpgsql(connectionString)
 .UseSnakeCaseNamingConvention()
 .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
 .EnableSensitiveDataLogging());


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequireUppercase = false;
})
	.AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
