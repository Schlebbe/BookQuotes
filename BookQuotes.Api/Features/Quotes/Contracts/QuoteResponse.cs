namespace BookQuotes.Api.Features.Quotes.Contracts
{
    public class QuoteResponse
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
    }
}
