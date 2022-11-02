using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{

    private class JsonData
    {
        public string gender;
        public string age;
        public string targetGender;
        public string target;
        public string targetHeight;
    }

    private JsonData data = new JsonData();

    [SerializeField] private Text _genderText;
    [SerializeField] private Text _ageText;
    [SerializeField] private Text _targetGenderText;
    [SerializeField] private Text _targetText;
    [SerializeField] private Text _targetHeightText;
    private TargetGender _targetGender;

    [SerializeField] private UIController _uiController;

    public TargetGender GetTargetGender()
    {
        return _targetGender;
    }

    private void Start()
    {
        if (GetData() != null)
        {
            SetProfile();
            _uiController.ShowCanvasWithoutTransition("Main");
        }
    }

    public void SetGender(string gender)
    {
        data.gender = gender;
    }
    
    public void SetAge(string age)
    {
        data.age = age;
    }
    
    public void SetTargetGender(string gender)
    {
        data.targetGender = gender;

        switch (gender)
        {
            case "Man":
                _targetGender = TargetGender.Male;
                break;
            case "Woman":
                _targetGender = TargetGender.Female;
                break;
            case "Everyone":
                _targetGender = TargetGender.Everyone;
                break;
        }
    }
    
    public void SetTarget(string target)
    {
        data.target = target;
    }
    
    public void SetTargetHeight(string height)
    {
        data.targetHeight = height;
    }

    public void SaveData()
    {
        var data = this.data;
        var json = JsonUtility.ToJson(data);
        Utils.SaveJson(json, $"{Application.persistentDataPath}/{Utils.ProfileDataFile}");
    }

    public enum TargetGender
    {
        Male,
        Female,
        Everyone
    }

    private JsonData GetData()
    {
        var json = Utils.GetJObject($"{Application.persistentDataPath}/{Utils.ProfileDataFile}");
        if(json == null) return null;
        return new JsonData
        {
            gender = json["gender"]?.ToString(),
            age = json["age"]?.ToString(),
            targetGender = json["targetGender"]?.ToString(),
            target = json["target"]?.ToString(),
            targetHeight = json["targetHeight"]?.ToString()
        };
    }

    public void SetProfile()
    {
        data = GetData();
        SetTargetGender(data.targetGender);
        _genderText.text = data.gender;
        _ageText.text = data.age;
        _targetGenderText.text = data.targetGender;
        _targetText.text = data.target;
        _targetHeightText.text = data.targetHeight;
    }
}
