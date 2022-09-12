using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Utils;
public class Card : MonoBehaviour
{
    public double animationRate;

    public double animationWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.CardLoadingAnimation += Loading;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Loading()
    {
        Debug.Log("load");
        StartCoroutine(In(animationWaitTime));
    }
    
    IEnumerator In(double waitTime)
    {
        yield return new WaitForSeconds((float)waitTime);
        Vector3 start = gameObject.transform.position;
        double now = 0;
        while (now < 1)
        {
            double radio = now / 1.0;
            // double positionY = 6 * AnimationUtils.EaseOutBack(radio);
            double positionY = 5.5 * AnimationUtils.EaseInOutBack(radio) + radio * 0.5;
            gameObject.transform.position =start + new Vector3(0, (float)positionY, 0);
            now += animationRate;
            yield return new WaitForFixedUpdate();
        }
        gameObject.transform.position =start + new Vector3(0, 6, 0);
    }
}
