using E_CommerceAPI.API.Configurations.ColumnWiters;
using E_CommerceAPI.API.Extensions;
using E_CommerceAPI.Application;
using E_CommerceAPI.Application.Validators.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.SignalIR;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Persistence;
using E_CommerceAPI.Persistence.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PostgreSQL;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Text;
using E_CommerceAPI.SignalIR.Hubs;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpContextAccessor();//clienten gelen request neticisinde olþturulan htpcontext nesnesine katmanlardaki klaslar üzerinde (bussines logic) eriþmemizi saðlayan bir servister.
        builder.Services.AddPersistenceService();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddSignalIRServices();

        builder.Services.AddStorage<LocalStorage>();
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200/", "https://localhost:4200/").AllowAnyHeader().AllowAnyMethod  ().AllowCredentials()));

        Logger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt")
            .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
                needAutoCreateTable: true,
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
            {"user_name", new UsernameColumnWriter()}
                })
            .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();
        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });

        builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = builder.Configuration["Token:Audience"],
                ValidIssuer = builder.Configuration["Token:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                NameClaimType = ClaimTypes.Name//Jwt üzerinde Name Claimme karþlýk gelen deðeri User.Identity.Name propertyinde elde edebiliriz.

            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
        app.UseSerilogRequestLogging();
        app.UseHttpLogging();
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseAuthentication();
        app.Use(async (context, next) =>
        {
            var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
            LogContext.PushProperty("user_name", username);
            await next();
        });
        app.MapControllers();
        app.MapHubs();
        app.Run();
    }
}