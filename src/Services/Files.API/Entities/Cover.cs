using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class Cover
    {
        [BsonRepresentation(BsonType.ObjectId)] // <-- for Mongo
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // <-- for EF
        public string Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Path => "covers/" + Name;

        public Cover(int postId, string name)
        {
            PostId = postId;
            Name = name;
        }

        public Cover()
        {
        }
    }
}