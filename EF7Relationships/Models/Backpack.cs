using System.Text.Json.Serialization;

namespace EF7Relationships.Models
{
    public class Backpack
    {
        public int Id { get; set; }
        public string Decription { get; set; }
        public int CharacterId { set; get; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
