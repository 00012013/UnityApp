using System.Text.Json.Serialization;

namespace UnityApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string PublishYear { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }

        public int AuthorId { get; set; }
    }
}
