using API.Filters;
using API.Modules;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Mapping;
using System.Reflection;
using API.MiddleWares;
using FluentValidation.AspNetCore;
using Service.Validations;
using Core.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Service.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));

builder.Services.AddControllers().AddFluentValidation(optipons =>
{
    optipons.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddSignalRServices();


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Host.UseServiceProviderFactory
        (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddIdentity<UserApp, RoleApp>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;
    Opt.Password.RequireLowercase= false;
    Opt.Password.RequireUppercase= false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));


builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignServic.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateIssuerSigningKey = true,//uretilecek token degerinin uygulamamiza ait bir deger oldugunu ifade eden security key verisinin dogrulanmasidir
        ValidateAudience = true,// olusturulacak token degerini kimlerin hangi sitelerin kullanicagaini belirlerdigimiz yerdir
        ValidateIssuer = true,// olusturulacak token degerini kimin dagittigini ifade eden alandir(bizim api miz)
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

    };

});

// Add services to the container.


//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMemoryCache();

    builder.Services.AddScoped(typeof(NotFoundFilter<,>));
    builder.Services.AddAutoMapper(typeof(MapProfile));

    





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();



var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
app.UseCors();
    
    app.UseCustomException();
    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

app.MapHubs();

    app.Run();

