using LibraryM.Services.Interfaces;
using LibraryM.Services;
using Microsoft.Azure.Cosmos;
using LibraryM.Middleware;


var builder = WebApplication.CreateBuilder(args);


var cosmosEndpoint = builder.Configuration["Cosmos_Endpoint"];
var cosmosKey = builder.Configuration["Cosmos_Key"];

builder.Services.AddSingleton<CosmosClient>(sp =>
    new CosmosClient(cosmosEndpoint, cosmosKey));


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILibrarianService, LibrarianService>();
builder.Services.AddScoped<IBorrowReturnService, BorrowReturnService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();

