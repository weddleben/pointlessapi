
namespace Magic
{class MagicEight
{
    public static string responses()
    {
        List<string> responses = new List<string> {
            "It is certain",
            "Reply hazy, try again",
            "Don’t count on it",
            "It is decidedly so",
            "Ask again later",
            "My reply is no",
            "Without a doubt",
            "Better not tell you now",
            "My sources say no",
            "Yes definitely",
            "Cannot predict now",
            "Outlook not so good",
            "You may rely on it",
            "Concentrate and ask again",
            "Very doubtful",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Yes",
            "Signs point to yes",
            "ngmi"};
        
        int length = responses.Count();
        Random random = new Random();
        int index = random.Next(0, length);
        return responses[index];
    }
}
}