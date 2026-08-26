using System.ComponentModel.DataAnnotations;

namespace BookQuotes.Api.Features.Books.Contracts
{
    public class CreateBookRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publication date is required.")]
        public DateTime? PublicationDate { get; set; }
    }
}
