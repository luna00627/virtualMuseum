using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MongoDBManager : MonoBehaviour
{
    private static MongoDBManager instance;
    private MongoClient client;
    private IMongoDatabase database;

    private void Start()
    {
        string connectionString = "mongodb+srv://popo:K5q4fl0en5NzhkLq@unity.yrrt9gw.mongodb.net/?retryWrites=true&w=majority&appName=unity";
        client = new MongoClient(connectionString);
        database = client.GetDatabase("海洋館");
    }

    public static MongoDBManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("MongoDBManager instance has not been initialized. Make sure it's attached to an active GameObject in the scene.");
            }
            return instance;
        }
    }

    public IMongoCollection<Comment> GetCommentsCollection(string exhibitId)
    {
        return database.GetCollection<Comment>($"comments_{exhibitId}");
    }

    public async Task AddComment(string exhibitId, Comment comment)
    {
        var commentsCollection = GetCommentsCollection(exhibitId);
        await commentsCollection.InsertOneAsync(comment);
    }

    public async Task<List<Comment>> GetComments(string exhibitId)
    {
        var commentsCollection = GetCommentsCollection(exhibitId);
        var filter = Builders<Comment>.Filter.Eq("ExhibitId", exhibitId);
        return await commentsCollection.Find(filter).ToListAsync();
    }
}
