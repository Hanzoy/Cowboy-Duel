using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Utils;
public class Card : MonoBehaviour
{
    public double animationInTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(In());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator In()
    {
        Vector3 start = gameObject.transform.position;
        double now = 0;
        while (now < animationInTime)
        {
            double radio = now / animationInTime;
            double positionY = 6 * AnimationUtils.EaseOutBack(radio);
            gameObject.transform.position =start + new Vector3(0, (float)positionY, 0);
            Debug.Log(gameObject.transform.position);
            now += Time.deltaTime;
            yield return 0;
        }
        gameObject.transform.position =start + new Vector3(0, 6, 0);
    }
}
