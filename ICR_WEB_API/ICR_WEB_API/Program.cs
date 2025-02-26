using ICR_WEB_API.Service.BLL.Interface;
using ICR_WEB_API.Service.BLL.Repository;
using ICR_WEB_API.Service.BLL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<ICRSurveyDBContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddScoped<IQuestionRepo, QuestionsRepo>();
builder.Services.AddScoped<IAnswerRepo, AnswerRepo>();

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
