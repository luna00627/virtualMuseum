using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Comment
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string ExhibitId { get; set; }
    public string Text { get; set; }
    public string UserName { get; set; }
    public int AvatarIndex { get; set; }
    public DateTime CreatedAt { get; set; }
}
