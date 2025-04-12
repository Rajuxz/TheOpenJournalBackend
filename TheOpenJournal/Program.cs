using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TheOpenJournal.Data;
using TheOpenJournal.Mapper;
using TheOpenJournal.Middlewares;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Implementations;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Implementation;
using TheOpenJournal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
//Serilog Configuration
var logger = new LoggerConfiguration()
    .MinimumLevel.Error() 
    .WriteTo.Console()
    .WriteTo.File("Logs/OpenJournalLogs.txt",rollingInterval: RollingInterval.Day) //Create a new file in new day and write the logs here.
    .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//Using NpgSql for database connectivity
string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(ConnectionString);
});

//-----------------------[IDENTITY FRAMEWORK CONFIGURATION]----------------------//

builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


//Disabling auto validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
//-----------------------[]-------------------------------------------------------//
builder.Services.AddAutoMapper(typeof(CategoryMapper));

// ----------------------[ Add Dependencies for Repository Here ] -------------- //
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository,TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
// ----------------------[ Add Dependencies for Services Here ] -------------- //
builder.Services.AddScoped<IAuthServices,AuthServices>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryServices, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUtilityService,UtilityService>();
builder.Services.AddScoped<ILikeService,LikeService>();

//WebHostEnvironment
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();







//---------------------[Setting up JWT Authentication]   ---------------------------- //
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.HttpContext.Request.Cookies["Jwt"];
            context.Token = token;
            return Task.CompletedTask;
        }
    };
});

//formsize

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024;
});

//-------------------------[Add Cors here] ------------------------------------------- //
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    builder.WithOrigins("http://localhost:5173")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
