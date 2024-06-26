using ChocoLink.Application.Services;
using ChocoLink.Data.EntityFramework;
using ChocoLink.Data.Repository;
using ChocoLink.Domain.Interfaces;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;
using ChocoLink.Infra.EmailService;
using ChocoLink.Infra.ServiceToken.Models;
using ChocoLink.Infra.ServiceToken;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddScoped<IInviteRepository, InviteRepository>();
builder.Services.AddScoped<IInviteService, InviteService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.Configure<Token>(
    builder.Configuration.GetSection("token"));

builder.Services.Configure<EmailConfig>(
    builder.Configuration.GetSection("EmailConfig"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(x => x.InjectStylesheet("/DarkSwagger/SwaggerDark.css"));
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();
app.MapControllers();

app.Urls.Add("http://0.0.0.0:5092");

app.Run();
