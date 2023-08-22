using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using GAID.Api.Configuration;
using GAID.Application.Attachment;
using GAID.Application.Authorization;
using GAID.Application.Email;
using GAID.Application.Repositories;
using GAID.Domain;
using GAID.Shared;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Binds appsettings.json
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
AppSettings.Instance = appSettings;

// Runtime configuring
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(appSettings.Cors.Origins.Split(';',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(appSettings.ConnectionStrings.SqlServer);
});
builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());
builder.Services.ConfigurationAuthentication();
builder.Services.ConfigureJwtToken();
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserContext>(x =>
    UserContext.Build(x.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User));
builder.Services.AddScoped<SmtpClient>(_ => new SmtpClient(AppSettings.Instance.Smtp.Server, 587)
{
    EnableSsl = true,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential(AppSettings.Instance.Smtp.Email, AppSettings.Instance.Smtp.Password)
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHangfire();
//
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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