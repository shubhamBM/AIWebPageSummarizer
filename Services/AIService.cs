using Google.GenAI;
using Microsoft.Extensions.Configuration;
using Google.GenAI.Types;
namespace AIWebPageSummarizer.Services
{
    public class AIService
    {
        private readonly string _apiKey;

        public AIService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _apiKey = configuration["Gemini:ApiKey"] ?? throw new Exception("Gemini API Key not found.");
        }

        public async Task<string> SummarizeImageAsync(string imagePath)
        {
            var client = new Client(apiKey: _apiKey);

            byte[] imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            var response = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
               contents: new List<Content>
{
    new Content
    {
        Parts = new List<Part>
        {
            Part.FromText(
                    @"You are an AI webpage summarizer.

                    Analyze the screenshot and provide:

                    1. A short summary (maximum 4 sentences)

                    2. Main topics (bullet points)

                    3. Important buttons, menus or actions visible

                    Keep the response concise and under 200 words."),
            Part.FromBytes(imageBytes, "image/png")
        }
    }
});

            return response.Text;
        }
    }
}