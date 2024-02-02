using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Note
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string User { get; set; }
    }
}
