using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class File
    {
        public File(string name, string type)
        {
            Name = name;
            Type = type;
        }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //todo post
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path => "Files/" + Name;

        public File()
        {
        }
    }
}