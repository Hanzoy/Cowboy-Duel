using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Utils;

namespace Script
{
    public class Poncho : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundRedGameObject;
        public Colors backgroundColor;

        private int _click;

        public void AddClick(int click = 2)
        {
            _click += click;
        }
        // Start is called before the first frame update
        void Start()
        {
            // ChangeColor(Colors.Green);
            // LoadingCard();
            _click = 2;
        }

        // Update is called once per frame
        void Update()
        {
            _CheckUserInput();
        }

        private void _CheckUserInput()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(ray, out raycastHit, 100, LayerMask.GetMask("Card")))
                    {
                        Card card = raycastHit.transform.GetComponent<Card>();
                        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved ||
                            touch.phase == TouchPhase.Stationary)
                        {
                            card.Hover();
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            // Debug.Log(i);
                            if (_click > 0)
                            {
                                if(card.Flip())
                                    _click--;
                            }
                        }
                    }
                }
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit mouseRaycastHit;
                if (Physics.Raycast(mouseRay, out mouseRaycastHit, 100, LayerMask.GetMask("Card")))
                {
                    Card card = mouseRaycastHit.transform.GetComponent<Card>();
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (_click > 0)
                        {
                            if(card.Flip())
                                _click--;
                        }
                    }
                    else
                    {
                        card.Hover();
                    }
                }
            }
        }

        public void ChangeColor(Colors colors)
        {
            backgroundColor = colors;
            backgroundRedGameObject.SetActive(backgroundColor == Colors.Red);
        }

        public void ChangeColor()
        {
            backgroundColor = backgroundColor == Colors.Green ? Colors.Red : Colors.Green;
            ChangeColor(backgroundColor);
        }

        public void LoadingCard()
        {
            EventHandler.CallCardLoadingAnimation();
        }
    }
}
