
var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.RegisterpipelineComponents(typeof(Program));

app.Run();
