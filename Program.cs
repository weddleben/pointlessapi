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

app.MapGet("/", () =>
{
    var endpoints = new List<Endpoints>
    {
        new Endpoints {endpoint = "/", name = "Home", description = "Welcome to my pointless API"},
        new Endpoints {endpoint = "/weather", name = "Random Weather", description = "Get the current weather from a random city in the USA."},
        new Endpoints {endpoint = "/magic8", name="Magic 8 Ball", description = "Get the answer to all your burning questions."},
        new Endpoints {endpoint = "/talktothehand", name="Talk to the Hand (no)", description="There's always another way to say no."},
        new Endpoints {endpoint = "/istiktokshutdown", name = "Is TikTok Shut Down", description = "The latest, up-to-date news on whether TikTok is shut down or not."},
        new Endpoints {endpoint = "/scramble?word=", name = "Vowel Scrambler", description = "Substitute a taco emoji for all vowels in the word."},
        new Endpoints {endpoint = "/istwitterfunctioningasintended", name = "Is Twitter Functioning as Intended", description = "Find out whether Twitter is working or not."},
        new Endpoints {endpoint = "/twitter?username=", name="Twitter Redirect", description="Redirect to a twitter profile."},
        new Endpoints {endpoint = "/movieclub", name = "Movie Club Endpoints", description = "Find all the #movieClub endpoints"},
        new Endpoints {endpoint = "/alphabet", name = "Frontend Alphabet API", description = "Letters are hard. There's an API for that."}
    };
    return Results.Json(new { endpoints = endpoints });
});

app.MapMethods("/", new[] { "POST", "PUT", "DELETE", "PATCH", "OPTIONS", "HEAD" }, (HttpContext context) =>
{
    var method = context.Request.Method;
    return Results.Json(new { message = $"Congratulations on your {method} request!"}, statusCode: 200);
});

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
    string response = randomize.MagicEight.responses();
    return Results.Json(new { message = response });
});

app.MapGet("/istiktokshutdown", () => 
{
    return Results.Json(new { message = "I don't even know any more..." });
    }
);

// app.MapPost("/ntl", static async () =>
// {
//    string title = await Icecast.Icecast.NowPlaying();
    

//     return Results.Json(new {title = title});
// }
// );

app.MapGet("/twitter", (HttpContext context) => {
    string? username = context.Request.Query["username"];

    return Results.Redirect($"https://twitter.com/{username}");
}
);

app.MapGet("/movieclub", ()=> {
string movies = "iqdit.cc/c7185b8";
string reviews = "iqdit.cc/fdce562";
string members = "iqdit.cc/e3f1a85";
return Results.Json(new {movies = movies, reviews = reviews, members = members});
}
);

app.MapGet("/talktothehand", () => {
    string no = randomize.TalkToTheHand.responses();
    return Results.Json(new {answer = no});
});

app.MapGet("istwitterfunctioningasintended", async () =>
{
    HttpClient client = new HttpClient();
    string url = "https://twitter.com";
    HttpResponseMessage response = await client.GetAsync(url);
    int statusCode = (int)response.StatusCode;
    if (statusCode == 200)
    {
        return Results.Json(new { answer = "The status code is 200, but who knows." });
    }
    return Results.Json(new { answer = $"The status code is {statusCode}, ngmi." });
});

app.MapGet("/weather", async () =>
{
    string forecast = await Weather.RunAsync();
    return Results.Json(new { forecast = forecast });
}
);

app.MapGet("/elbowfetish", () =>
{
    return Results.Redirect("https://twitter.com/LaFemmeFrank");
});

app.MapGet("/alphabet", () =>
{
    return Results.Redirect("https://www.costcobros.com/PaaS/alphabet");
});

app.MapFallback(() => Results.NotFound(new { Message = "What are you looking for?? Suggest new pointless endpoint ideas to: twitter.com/BenjaminMuses" }));

app.Run();