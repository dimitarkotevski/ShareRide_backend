
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShareRide.API.Config;
using ShareRide.API.Converter;
using ShareRide.API.DataContext;
using ShareRide.API.Repository;
using ShareRide.API.Security.Hashing;
using ShareRide.API.Services.Impl;
using ShareRide.API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<HashingPassword>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddTransient<VerificationCodeRepository>();
builder.Services.AddTransient<MailMessage>();
builder.Services.AddTransient<EmailConfig>();
builder.Services.AddTransient<RandomVerificationCode>();
builder.Services.AddTransient<IRoleService,RoleService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<ConverterObjectsFromDataBase>();
builder.Services.AddSession();
builder.Services.AddMemoryCache();

ServiceProvider provider = builder.Services.BuildServiceProvider();
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
    opts
        .UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("defaultConnectionString"))
    );

//Add Authentication
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
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//Config the HTTP request pipeline
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Run App
app.Run();