using FixTimeBack.Custom;
using FixTimeBack.Data;
using FixTimeBack.Interfaces;
using FixTimeBack.Repository;
using FixTimeBack.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Construir el servicio de swagger para poder tener la autorizacion como parametro
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FixTime API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token en el formato: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]!))
    };
}


);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
builder.Services.AddSingleton<Utilidades>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUsuarioRepository, RepositorioUsuarios>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ICitasRepository, RepositorioCitas>();
builder.Services.AddScoped<ICitasService, CitasService>();

builder.Services.AddScoped<ITallerRepository, RepositorioTaller>();
builder.Services.AddScoped<ITallerService, TallerService>();

builder.Services.AddScoped<IServicioRepository, RepositorioServicios>();
builder.Services.AddScoped<IServicioService, ServiciosService>();

builder.Services.AddScoped<IVehiculoRepository, RepositorioVehiculo>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.Run();
