using System.IO;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class FileInfo
    {
        public FileInfo(string name, string type)
        {
            Name = name;
            Type = type;
        }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path => "Files/" + Name;

        public FileInfo()
        {
        }
    }
}