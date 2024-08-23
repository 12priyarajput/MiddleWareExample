using MiddlewareExample.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

//middleware1
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello\n");
    await next(context);
});

//middleware2
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello again\n");
    await next(context);// if not use next middleware will not call
    await context.Response.WriteAsync("Hello again from mv2\n");
});

//CustomMiddlware
app.UseMiddleware<MyCustomMiddleware>();
//anpther way
app.UseMyCustomMiddleware();//Middlleware with extension method

//middleware3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("World\n");
});

app.Run();
//app.run not forwared request furhter
//app.use forwared request further with next parameter