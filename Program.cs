using LibraryM.Services.Interfaces;
using LibraryM.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register CosmosClient
builder.Services.AddSingleton<CosmosClient>(sp =>
    new CosmosClient(
        "https://localhost:8081",
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
    ));

// ✅ Register your services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILibrarianService, LibrarianService>();
builder.Services.AddScoped<IBorrowReturnService, BorrowReturnService>();

// ✅ Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
