using System.Text.Json.Serialization;

namespace AIWebPageSummarizer.Models
{
    public class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public List<Candidate>? Candidates { get; set; }
    }

    public class Candidate
    {
        [JsonPropertyName("content")]
        public ResponseContent? Content { get; set; }
    }

    public class ResponseContent
    {
        [JsonPropertyName("parts")]
        public List<ResponsePart>? Parts { get; set; }
    }

    public class ResponsePart
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}