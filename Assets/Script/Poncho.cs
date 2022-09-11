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

        // Start is called before the first frame update
        void Start()
        {
            ChangeColor(Colors.Green);
        }

        // Update is called once per frame
        void Update()
        {
        
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
    }
}
