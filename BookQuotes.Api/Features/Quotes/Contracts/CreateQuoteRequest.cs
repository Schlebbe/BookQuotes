using System.ComponentModel.DataAnnotations;

namespace BookQuotes.Api.Features.Quotes.Contracts
{
    public class CreateQuoteRequest
    {
        [Required]
        [StringLength(2000)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;
    }
}
