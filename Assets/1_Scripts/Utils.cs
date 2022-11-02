using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class Utils
{

    public static string ProfileDataFile = "profile.json";
    public static string HistoryDataFile = "history.json";
    
    public static void SaveJson(string json, string path)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, json);
        }
        else
        {
            File.Delete(path);
            SaveJson(json, path);
        }
    }

    public static JObject GetJObject(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log($"File {path} not found!");
            return null;
        }
        return JObject.Parse(File.ReadAllText(path));
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }
}
