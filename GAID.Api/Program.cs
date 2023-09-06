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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DateOnlyJsonConverter = GAID.Shared.DateOnlyJsonConverter;

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
builder.Services.AddHttpClient();

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

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    x.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
//config get file name in header
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
    context.Response.Headers.Add("x-frame-options", "DENY");
    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
    await next.Invoke();
});

app.Run();