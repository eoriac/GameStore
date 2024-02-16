using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.API;

public class Game
{
    // POCO = Plain Old Class Object
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
}



/*

var gameDocument = new BsonDocument
{
    {"_id", "asdasd"},
    {"name", "asdasd"},
};

*/