using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using Script.Utils;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    public double animationRate;

    public double animationWaitTime;

    public int index;

    public GameObject hoverGameObject;
    public GameObject contentGameObject;
    
    public double flipHight;
    public double flipAnimationRate;
    public double collectAnimationRate;
    private bool _onFlip = false;
    private bool _onHover;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        EventHandler.CardLoadingAnimation += Loading;
    }

    // Update is called once per frame
    void Update()
    {
        hoverGameObject.SetActive(_onHover);
        _onHover = false;
    }

    void Loading()
    {
        StartCoroutine(In(animationWaitTime * index));
    }

    void Settlement(bool identical)
    {
        if (identical)
        {
            var target = GameManager.Instance().ControllerIsMyself() ? GameObject.Find("poncho/collection_target/myself").transform.position : GameObject.Find("poncho/collection_target/other").transform.position;

            StartCoroutine(Collect(target));
        }
        else
        {
            StartCoroutine(FlipOff());
        }
        _onFlip = false;
        EventHandler.CardSettlement -= Settlement;
    }
    
    void Init()
    {
        int theIndex = index > 11 ? index + 1 : index;
        transform.localPosition = new Vector3(theIndex % 5, -theIndex / 5 - 6, 0);
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

    public bool Flip()
    {
        // Debug.Log("flip");
        if (!_onFlip)
        {
            StartCoroutine(FlipOn());
            _onFlip = true;
            return true;
        }
        return false;
    }

    IEnumerator FlipOn()
    {
        CardType cardType = GameManager.Instance().FindCardContent(index);
        Vector3 start = gameObject.transform.position;
        double now = 0;
        bool flag = true;
        while (now < 1)
        {
            double radio = now / 1.0;
            if (radio < 0.45)
            {
                gameObject.transform.position = start + new Vector3(0, (float)(flipHight * radio / 0.45), 0);
                gameObject.transform.rotation = Quaternion.Euler(0, (float)(86 * radio / 0.45), 0);
            }
            else if (flag)
            {
                ChangeCardContent(cardType);
                flag = false;
            }
            else
            {
                gameObject.transform.position = start + new Vector3(0, (float)(flipHight * (1-radio) / 0.55), 0);
                gameObject.transform.rotation = Quaternion.Euler(0, (float)(86 * (1-radio) / 0.55), 0);
            }
            
            now += flipAnimationRate;
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = start;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        EventHandler.CardSettlement += Settlement;
    }

    IEnumerator FlipOff()
    {
        Vector3 start = gameObject.transform.position;
        double now = 0;
        bool flag = true;
        while (now < 1)
        {
            double radio = now / 1.0;
            if (radio < 0.45)
            {
                gameObject.transform.position = start + new Vector3(0, (float)(flipHight * radio / 0.45), 0);
                gameObject.transform.rotation = Quaternion.Euler(0, (float)(86 * radio / 0.45), 0);
            }
            else if (flag)
            {
                ChangeCardContent();
                flag = false;
            }
            else
            {
                gameObject.transform.position = start + new Vector3(0, (float)(flipHight * (1-radio) / 0.55), 0);
                gameObject.transform.rotation = Quaternion.Euler(0, (float)(86 * (1-radio) / 0.55), 0);
            }
            
            now += flipAnimationRate;
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = start;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator Collect(Vector3 target)
    {
        var position = gameObject.transform.position;
        var direction = (target - position).normalized;
        double now = 0;
        while (now < 1)
        {
            double radio = now / 1.0;
            gameObject.transform.position = position * (float)(1-radio) + target * (float)(radio);
            now += collectAnimationRate;
            yield return new WaitForFixedUpdate();
        }
        Init();
        ChangeCardContent();
    }
    
    public void Hover()
    {
        _onHover = true;
        hoverGameObject.SetActive(_onHover);
    }

    void ChangeCardContent(CardType cardType)
    {
        string path = "Card/";
        switch (cardType)
        {
            case CardType.Defend:
                path = path + "card_defend";
                break;
            case CardType.Load:
                path = path + "card_load";
                break;
            case CardType.Load3:
                path = path + "card_load_3";
                break;
            case CardType.Ricochet:
                path = path + "card_ricochet";
                break;
            case CardType.Shoot:
                path = path + "card_shoot";
                break;
            case CardType.Shoot2:
                path = path + "card_shoot_2";
                break;
        }

        Sprite contentSprite = Resources.Load<Sprite>(path);
        SpriteRenderer spriteRenderer = contentGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = contentSprite;
        contentGameObject.SetActive(true);
    }

    void ChangeCardContent()
    {
        contentGameObject.SetActive(false);
    }

}
