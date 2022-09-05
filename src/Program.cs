using Microsoft.EntityFrameworkCore;
using SleekBoard.Database;
using SleekBoard.Dtos;
using SleekBoard.Entities;
using SleekBoard.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SleekBoardContext>(opt =>
    opt.UseInMemoryDatabase("SleekBoardDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Board, BoardOutput>();
    config.CreateMap<BoardItem, BoardItemOutput>();
});

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IBoardItemRepository, BoardItemRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
