using Microsoft.EntityFrameworkCore;
using InterviewCalendarApi.Data;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Services;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Repositories;
using InterviewCalendarApi.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CalendarDbContext>(opt =>
    opt.UseInMemoryDatabase("InterviewCalendar"));

builder.Services.AddScoped<IBaseRepository<Interviewer>, InterviewerRepository>();
builder.Services.AddScoped<IBaseRepository<Candidate>, CandidateRepository>();
builder.Services.AddScoped<ISlotRepository<InterviewerSlot>, InterviewerSlotRepository>();
builder.Services.AddScoped<ISlotRepository<CandidateSlot>, CandidateSlotRepository>();

builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
