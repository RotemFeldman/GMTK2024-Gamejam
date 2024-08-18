using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{

    public SpriteRenderer renderer;
    public float fadeSpeed = 10;
    private bool fadeOut = true;

    private float prevValue = 1;
    
    void Start()
    {
        renderer.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            if (renderer.color.a > 0)
            {
                prevValue -= fadeSpeed* Time.deltaTime;
                renderer.color = new Color(0, 0, 0, prevValue);
            }
        }
        else
        {
            prevValue += fadeSpeed* Time.deltaTime;
            renderer.color = new Color(0, 0, 0, prevValue);
        }
    }

    public void FadeIn()
    {
        fadeOut = false;
    }


}
