using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [Serializable]
    public struct IObject
    {
        public GameObject gameObject;
        public string name;
    }

    [SerializeField] private AudioClip _clip;
    [SerializeField] private IObject[] _objects;
    [SerializeField] private UnityEvent _onStart;
    [SerializeField] private float _delayTime;

    [SerializeField] private GameObject _transitionObject;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        _onStart.Invoke();
    }
    private IEnumerator ShowTransition(string name)
    {
        AudioManager.getInstance().PlayAudio(_clip);
        
        var transition = Instantiate(_transitionObject).GetComponent<UITransition>();
        transition.LengthAnimation();

        yield return new WaitForSeconds(transition.LengthAnimation()/2f);
        
        foreach (var obj in _objects)
            obj.gameObject.SetActive(obj.name.Equals(name));
    }

    public void DelayShowCanvas(string name)
    {
        StartCoroutine(DelayShow(name));
    }

    private IEnumerator DelayShow(string name)
    {
        yield return new WaitForSeconds(3);
        ShowCanvas(name);
    }
    
    public void ShowCanvas(string name)
    {
        if (_transitionObject)
        {
            StartCoroutine(ShowTransition(name));
            return;
        }
        AudioManager.getInstance().PlayAudio(_clip);
        foreach (var obj in _objects)
            obj.gameObject.SetActive(obj.name.Equals(name));
    }
    
    public void ShowCanvasWithoutTransition(string name)
    {
        AudioManager.getInstance().PlayAudio(_clip);
        foreach (var obj in _objects)
            obj.gameObject.SetActive(obj.name.Equals(name));
    }

    public void ShowCanvasWithDelay(string name)
    {
        StartCoroutine(ShowWithDelay(name));
    }

    private IEnumerator ShowWithDelay(string name)
    {
        yield return new WaitForSeconds(_delayTime);
        ShowCanvas(name);
    }

    public void HideCanvases()
    {
        AudioManager.getInstance().PlayAudio(_clip);
        foreach (var obj in _objects)
            obj.gameObject.SetActive(false);
    }
}
