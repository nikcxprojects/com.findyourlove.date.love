using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;

    public Transform content;
    public HistoryViews HistoryViews;

    private Image _image;
    private Text _text;

    private HistoryViews.Data _data = new HistoryViews.Data();

    private void OnEnable()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<Text>();
    }
    
    public void OpenProfile()
    {
        var obj = Instantiate(_prefab, content);
        obj.transform.Find("Info").Find("NameText").GetComponent<Text>().text = _text.text;
        obj.transform.Find("Image").Find("Photo").GetComponent<Image>().sprite = _image.sprite;
        var btns = obj.transform.Find("Info").Find("Buttons");
        btns.Find("ButtonClose").GetComponent<Button>().onClick.AddListener(()=> Close(obj));
        btns.Find("ButtonLike").GetComponent<Button>().onClick.
            AddListener(()=> OnValueChanged((int)History.SwipeType.Like, obj));
        btns.Find("ButtonDislike").GetComponent<Button>().onClick.
            AddListener(()=> OnValueChanged((int)History.SwipeType.Dislike, obj));
    }

    private void Close(GameObject obj)
    {
        Destroy(obj);
        HistoryViews.GenerateViews();
    }
    
    public void Init(HistoryViews.Data data)
    {
        _data.path = data.path;
        _data.title = data.title;
    }

    private void OnValueChanged(int type, GameObject gobj)
    {
        var json = Utils.GetJObject($"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
        var array = (JArray) json["array"];
        
        foreach (var obj in array)
        {
            var d = new HistoryViews.Data
            {
                path = (string) obj["Path"],
                title = (string) obj["Title"]
            };
            
            if (d.path.Equals(_data.path) && d.title.Equals(_data.title))
            {
                obj["Type"] = type;
            }
        }

        Utils.DeleteFile($"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
        Utils.SaveJson(json.ToString(), $"{Application.persistentDataPath}/{Utils.HistoryDataFile}");
        Close(gobj);
    }
}
