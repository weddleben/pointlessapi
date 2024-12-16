var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapMethods("/", new[] { "GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS", "HEAD" }, (HttpContext context) =>
{
    var method = context.Request.Method;
    return Results.Json(new { message = $"Congratulations on your {method} request!"}, statusCode: 200);
});

app.MapGet("/sams-version-is-better", () => 
{
    return Results.Redirect("https://pointlessapi.azurewebsites.net/randomhttpcode.json");
}
);

app.MapFallback(() => Results.NotFound(new { Message = "What are you looking for?? Suggest new pointless endpoint ideas to: twitter.com/ben__weddle" }));

app.Run();