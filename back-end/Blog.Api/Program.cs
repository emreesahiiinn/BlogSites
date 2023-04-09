using Blog.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()));

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:5001"; // IdentityServer4 URL
        options.Audience = "portal-resource"; // Resource name defined in IdentityServer4
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

// DB connections
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Blog.Data")));

builder.Services.AddScoped<DbContext>(provider => provider.GetService<BlogContext>());

var app = builder.Build();

// Configure the HTTP request pipeline
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();

//app.UseCors(MyAllowSpecificOrigins);
app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
