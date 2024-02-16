using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// User Entity
/// </summary>
public class User
{
    /// <summary>
    /// User Id
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// User Name
    /// </summary>
    [BsonElement("name")]
    public string? Name { get; set; } = null!;

    /// <summary>
    /// User Email
    /// </summary>
    [BsonElement("email")]
    public string? Email { get; set; } = null!;
}
