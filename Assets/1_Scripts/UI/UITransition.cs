using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{

    private Animation _animation;

    public float LengthAnimation()
    {
        if(!_animation) _animation = GetComponent<Animation>();
        return _animation.clip.length;
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
