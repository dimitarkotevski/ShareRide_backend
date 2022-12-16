
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShareRide.API.DataContext;
using ShareRide.API.Security.Hashing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<HashingPassword>();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
//for frontend

builder.Services.AddCors(option =>
{
    var frontendUrl = configuration.GetValue<string>("ShareRide.CLI");
    Console.Out.WriteLine(frontendUrl);
    option.AddDefaultPolicy(b =>
    {
        b.WithOrigins(frontendUrl).AllowAnyMethod().AllowAnyHeader();
    });
});
//connect to DataBase
builder.Services.AddDbContext<ShareRideDbContext>(
    opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnectionString"))
    );

//add authentification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };   
    }
);
    var app = builder.Build();            

//Config the HTTP request pipeline
app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();