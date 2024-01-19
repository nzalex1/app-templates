var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var helows = new[]
{
    "Hello world", "Bonjour le monde", "Hola Mundo", "Salut Lume", "Hei maailma"
};


app.MapGet("/", () =>
{
    var root =  Enumerable.Range(1, 5).Select(index =>
        new Hello
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            helows[Random.Shared.Next(helows.Length)]
        ))
        .ToArray();
    return root;
})
.WithName("root")
.WithOpenApi();

app.MapGet("/hello", () =>
{
    var hello =  Enumerable.Range(1, 5).Select(index =>
        new Hello
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            helows[Random.Shared.Next(helows.Length)]
        ))
        .ToArray();
    return hello;
})
.WithName("hello")
.WithOpenApi();

app.Run();

record Hello (DateOnly Date, string? Greeting)
{
}
