using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{

    private float time = 0;
    private AudioSource audio;
    
    private void OnEnable()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetInt("VolumeMusic", 1);
        time = GetComponent<AudioSource>().clip.length;
    }

    private void Update()
    {
        audio.volume = PlayerPrefs.GetInt("VolumeMusic", 1);
    }

    public void Init(float time)
    {
        StopAllCoroutines();
        this.time = time;
        Destroy();
    }

    public void Destroy()
    {
        StartCoroutine(DestroyGameObject());
    }
    
    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}