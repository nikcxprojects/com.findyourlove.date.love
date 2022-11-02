using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryViews : MonoBehaviour
{
    [SerializeField] private Transform _liked;
    [SerializeField] private Transform _disliked;
    [SerializeField] private Transform _all;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _content;

    public class Data
    {
        public string title;
        public string path;
    }

    public void GenerateViews()
    {
        foreach (Transform o in _all)
            Destroy(o.gameObject);
        
        foreach (Transform o in _disliked)
            Destroy(o.gameObject);
        
        foreach (Transform o in _liked)
            Destroy(o.gameObject);
        
        var json = Utils.GetJObject($"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
        if (json == null) return;

        var array = (JArray) json["array"];

        var a = array.Reverse();
        
        foreach (var obj in a)
        {
            var type = (int) obj["Type"];
            var d = new Data()
            {
                path = (string) obj["Path"],
                title = (string) obj["Title"]
            };
            Generate(_all, d);
            Generate(type == 0 ? _liked : _disliked, d);
        }
    }

    private void Generate(Transform content, Data data)
    {
        var obj = Instantiate(_prefab, content);
        obj.GetComponent<HistoryView>().Init(data);
        obj.GetComponent<HistoryView>().content = _content;
        obj.GetComponent<HistoryView>().HistoryViews = this;
        obj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(data.path);
        obj.transform.Find("Text").GetComponent<Text>().text = data.title;
    }
}
