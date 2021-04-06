using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Files.API.Entities
{
    public class Link
    {
        private const string Url = "http://localhost:5004/Files/GetFile/";

        public Link(string fileId)
        {
            FileId = fileId;
            Token = TokenMaker.GetToken();
        }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileId { get; set; }
        public string Token { get; set; }
        public string Uri => Url + FileId + "/" + Token;

        public Link()
        {
        }
    }
}