var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allows all origins
              .AllowAnyMethod() // Allows all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader(); // Allows all headers
    });
});

var app = builder.Build();

app.UseCors("AllowAll");


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

app.MapGet("/scramble", (HttpContext context) =>
{
    var query = context.Request.Query["word"];
    string scrambled = new string(query.ToString().ToArray());
    string newString = "";
    List<char> repalceable = new List<char>{'a', 'e', 'i', 'o', 'u'};
    foreach(char letter in scrambled) {
        if (repalceable.Contains(letter)) {
            newString += "🌮";
        }
        else {
            newString += letter;
        }
    }
    // string result = new string(newString.ToArray());
    return Results.Json(new { message = newString });
});

app.MapGet("/magic8", () =>
{
    string response = Magic.MagicEight.responses();
    return Results.Json(new { message = response });
});

app.MapGet("/istiktokshutdown", () => 
{
    return Results.Json(new { message = "Yes! Go touch grass!" });
    }
);

app.MapFallback(() => Results.NotFound(new { Message = "What are you looking for?? Suggest new pointless endpoint ideas to: twitter.com/ben__weddle" }));

app.Run();