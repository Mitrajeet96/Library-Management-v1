using API.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors((options) =>
{
    options.AddPolicy("Library", (builder) =>
    {
        //builder.WithOrigins("http://localhost:5015/")
        //.AllowAnyHeader()
        //.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
        //.WithExposedHeaders("*");

        builder.AllowAnyOrigin(); 
        builder.AllowAnyHeader(); 
        builder.AllowAnyMethod();
    });
});


//DI
builder.Services.AddSingleton<IDataAccess, DataAccess>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// CORS
app.UseCors("Library");

app.UseAuthorization();

app.MapControllers();

app.Run();
