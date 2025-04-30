
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

    class TalkToTheHand
    {
        public static string responses()
        {
            List<string> responses = new List<string> {
            "Not now!",
            "No. Nope. Never.",
            "That was a no. Again.",
            "No, as per usual.",
            "Yes, but no.",
            "No. Not while I'm alive",
            "No. It's like yes but means no.",
            "Let me think about it. Actually, it's a no.",
            "No because it works for me.",
            "Absolutely! (not)",
            "As IF!",
            "Read the room. It's a no.",
            "I will answer you in Spanish: No.",
            "It's a no from me dog.",
            "How about no.",
            "Thinking....yeah it's still a no.",
            "Not now! (or ever)"
            };

            int length = responses.Count();
            Random random = new Random();
            int index = random.Next(0, length);
            return responses[index];
        }
    }
}