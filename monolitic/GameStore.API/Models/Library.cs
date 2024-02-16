using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.API;

public class Library
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("path")]
    public string? Path { get; set; }
}
