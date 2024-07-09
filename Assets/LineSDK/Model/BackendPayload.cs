using System;
using Line.LineSDK;
using UnityEngine;
[Serializable]
public class BackendPayload
{
    [SerializeField]
    public string type;
    [SerializeField]
    public LoginResult payload;
    public BackendPayload(string type, LoginResult payload)
    {
        this.type = type;
        this.payload = payload;
    }

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static BackendPayload fromJson(string json)
    {
        return JsonUtility.FromJson<BackendPayload>(json);
    }

    public static string wrapValue(string type, string payload)
    {
        var payloadObj = new BackendPayload(type, JsonUtility.FromJson<LoginResult>(payload));
        return payloadObj.toJson();
    }
}

