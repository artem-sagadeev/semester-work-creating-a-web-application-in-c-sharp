using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class Avatar
    {
        //todo avatar repository and controller
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CreatorId { get; set; }
        public string Path { get; set; }

        public Avatar(int creatorId, string name)
        {
            CreatorId = creatorId;
            Path = "Avatars/" + name;
        }

        public Avatar()
        {
        }
    }
}