using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using QuickTest.Application.Exams.CreateExam;
using QuickTest.Application.Exams.GetExams;
using QuickTest.Application.Groups.CreateGroup;
using QuickTest.Application.Groups.GetGroups;
using QuickTest.Application.Services;
using QuickTest.Application.Students.GetStudents;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Application.Users.Login.Services;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;
using QuickTest.Infrastructure.Services;
using QuickTest.Infrastructure.Utilities;
using QuickTest.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<JwtHandler>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();
builder.Services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
builder.Services.AddScoped<ISelectedStudentAnswerRepository, SelectedStudentAnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IPredefinedAnswerRepository, PredefinedAnswerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<CreateGroupHandler>();
builder.Services.AddTransient<ISchoolRepository, SchoolRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserContextService, UserContextService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMediatR(typeof(CreateExamHandler));
builder.Services.AddMediatR(typeof(GetExamsHandler));
builder.Services.AddMediatR(typeof(GetGroupsHandler));
builder.Services.AddMediatR(typeof(GetStudentsHandler));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddMemoryCache();

builder.Services.AddIdentity<User, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 7;
    options.Password.RequireDigit = true;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("Server=inzynierka2023.database.windows.net;Database=QuickTest;User Id=adminqt;Password=AdminQuickTest69;"
        , b => b.MigrationsAssembly("QuickTest.Infrastructure"));
    options.EnableSensitiveDataLogging(true);
});
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var app = builder.Build();
var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = serviceScopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    DbInitializer.FillDatabaseRandomData(context, userManager).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
