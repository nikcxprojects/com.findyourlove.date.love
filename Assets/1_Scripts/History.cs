using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SwipeableView;
using UnityEngine;

public static class History
{
    public static void Add(BasicCardData data, SwipeType swipe)
    {
        var json = Utils.GetJObject($"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
        var obj = new JObject
        {
            {"Title", data.title},
            {"Path", data.pathToPhoto},
            {"Type", (int)swipe}
        };
        var array = new JArray();
        if (json != null)
        {
            array = (JArray) json["array"];
            array.Add(obj);
        }
        else
        {
            json = new JObject();
            array.Add(obj);
            json.Add("array", array);
        }
        Utils.SaveJson(json.ToString(), $"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
    }

    public enum SwipeType
    {
        Like,
        Dislike
    }
}
