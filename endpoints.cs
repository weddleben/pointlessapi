public class Endpoints
{
    required public string endpoint { get; set; }
    required public string name { get; set; }
    required public string description { get; set; }
}

public class About
{
    public Dictionary<string, string> about = new Dictionary<string, string> {
    {"name", "Pointless API"},
    {"description", "The only pointless API you will ever need. Free to use however you like. No API keys, no CORS, no restrictions."},
    {"creator", "Created by Ben (https://twitter.com/BenjaminMuses)"}
    };
}