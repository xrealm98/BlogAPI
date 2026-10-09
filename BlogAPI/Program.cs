using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Repositories.Implementations;
using BlogAPI.Repositories.Interfaces;
using BlogAPI.Services.Auth;
using BlogAPI.Services.Auth.Interface;
using BlogAPI.Services.Category;
using BlogAPI.Services.Post;
using BlogAPI.Services.Post.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT así: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;           
    options.Password.RequireUppercase = false;        
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireLowercase = false;        

    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDBContext>();

var jwtKey = builder.Configuration["JWT:Key"];
var jwtIssuer = builder.Configuration["JWT:Issuer"]!;
var jwtAudience = builder.Configuration["JWT:Audience"]!;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
         OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,       
        ValidateAudience = true,         
        ValidateLifetime = true,         
        ValidateIssuerSigningKey = true, 
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
