using System;
using System.ComponentModel.DataAnnotations.Schema;
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
        [BsonRepresentation(BsonType.ObjectId)] // <-- for Mongo
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // <-- for EF
        public string Id { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public CreatorType CreatorType { get; set; }
        public string Path => "avatars/" + Name;

        public Avatar(int creatorId, string name, int creatorType)
        {
            CreatorId = creatorId;
            Name = name;
            CreatorType = creatorType switch
            {
                0 => CreatorType.User,
                1 => CreatorType.Project,
                2 => CreatorType.Company,
                _ => throw new ArgumentException()
            };
        }

        public Avatar()
        {
        }
    }
}