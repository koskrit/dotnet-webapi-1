using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Note
    {
        public long Id { get; set; }

        [MinLength(3), MaxLength(100)]
        public string Title { get; set; }

        [MinLength(3)]
        public string HtmlContent { get; set; }

        [ReadOnly(true)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public string User { get; set; }
    }
}
