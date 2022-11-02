using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsText : MonoBehaviour
{

    private Text text;
    [SerializeField] private string PlayerPrefsName;
    [SerializeField] private string startText;
    
    void Start()
    {
        text = GetComponent<Text>();
        text.text = startText + " " + PlayerPrefs.GetInt(PlayerPrefsName);
    }

}
