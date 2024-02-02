using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class NoteDTO
    {
        [MinLength(3), MaxLength(100)]
        public string Title { get; set; }

        [MinLength(3)]
        public string HtmlContent { get; set; }
    }
}
