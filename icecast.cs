
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace Icecast;
class Icecast
{

    private static async Task<string> CallIcecast()
    {
        HttpClient client = new HttpClient();
        string? icecastUsername = Environment.GetEnvironmentVariable("icecast_user");
        string? icecastPassword = Environment.GetEnvironmentVariable("icecast_pass");
        string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{icecastUsername}:{icecastPassword}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        string url = "http://npl.streamguys1.com/admin/stats.xml";
        try
        {

            HttpResponseMessage response = await client.GetAsync(url);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response as a string
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                return "error";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message);
            return "error";
        }
    }

    public static async Task<string> NowPlaying()
    {
        try
        {
            string iceResponse = await CallIcecast();
            XDocument doc = XDocument.Parse(iceResponse);
            var sources = doc.Descendants("source");
            foreach (var source in sources)
            {
                string? mount = source.Attribute("mount")?.Value;
                if (mount == "/live")
                {
                    string? title = source.Element("title")?.Value;
                    Console.WriteLine(title);
                    return title;
                }
            }
        }

        catch (Exception)
        {
            return "";
        }
        return "";
    }

}