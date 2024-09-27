using ExportXls.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConenction")));

var app = builder.Build();
app.MapRazorPages();
app.Run();
