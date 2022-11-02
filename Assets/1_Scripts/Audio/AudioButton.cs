using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private int volumeMusic;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    private Image image;

    void Start()
    {
        volumeMusic = PlayerPrefs.GetInt("VolumeMusic", 1);
        UpdateUI();
    }

    public void ChangeMusicVolume()
    {
        volumeMusic = volumeMusic == 0 ? 1 : 0;
        PlayerPrefs.SetInt("VolumeMusic", volumeMusic);
        UpdateUI();
    }
    
    public void ChangeMusicVolume(int volume)
    {
        volumeMusic = volume;
        PlayerPrefs.SetInt("VolumeMusic", volumeMusic);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(!on) return;
        if(!image) image = GetComponent<Image>();
        image.sprite = volumeMusic == 0 ? off : on;
    }
    
}
