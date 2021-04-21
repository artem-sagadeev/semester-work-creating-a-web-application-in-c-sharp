using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class File
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path => "Files/" + Name;

        public File(int postId, string name, string type)
        {
            PostId = postId;
            Name = name;
            Type = type;
        }
        
        public File()
        {
        }
    }
}