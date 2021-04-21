using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public enum CreatorType
    {
        User,
        Project,
        Company
    }
    
    public class Avatar
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public CreatorType CreatorType { get; set; }
        public string Path => "avatars/" + Name;

        public Avatar(int creatorId, string name)
        {
            CreatorId = creatorId;
            Name = name;
        }

        public Avatar()
        {
        }
    }
}