// 27 Sep 2023 :
// OTP Validation added
// PAN Number Validation added

using BusinessAssociateAPI.Extensions;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using DataAccess.Repository;
using Business.Contracts;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Entities;
using Business.Services;
using Business.Services.BA;
using DataAccess.Dto.Request;
using BusinessAssociateAPI.Validators;
using DataAccess.Dto.OTP;
using Business.Helpers;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);

builder.Logging.AddNLog(logPath).SetMinimumLevel(LogLevel.Trace);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true);
}));

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<ServiceHelper>();
//builder.Services.AddTransient<ServiceWrapper>();
builder.Services.AddTransient<IServiceWrapper, ServiceWrapper>();
builder.Services.AddTransient<BusinessAssociate>();
builder.Services.AddTransient<DtoWrapper>();
builder.Services.AddTransient<ErrorResponse>();
builder.Services.AddTransient<IServiceHelper, ServiceHelper>();
builder.Services.AddTransient<IBAService, BAService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<BAFlagCheckDto>();
builder.Services.AddTransient<BAPostReqDto>();
builder.Services.AddTransient<DocumentUploadDto>();
builder.Services.AddTransient<Docu_convertedBytesDto>();
builder.Services.AddTransient<IValidator<BAFlagCheckDto>, BA_Flag_Validator>();
builder.Services.AddTransient<IValidator<DocumentUploadDto>, DocumentValidator>();
builder.Services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddTransient<OtpDtoWrapper>();
builder.Services.AddTransient<OTPHelperClass>();


builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    // This middleware serves the Swagger documentation UI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Associate API V1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<JwtMiddlewareExtension>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
