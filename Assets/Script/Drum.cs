using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class Drum : MonoBehaviour
    {
        public GameObject[] slugs;
        private int _count;
        private Queue<Action> _channel;
        private const int Left = 1;
        private const int Right = 0;
        public GameObject drum;
        private void Start()
        {
            _channel = new Queue<Action>();
            
        }

        /**
         * 右旋
         */
        public void DextralRotation()
        {
            StartCoroutine(Rotation(Right));
        }
        
        
        /**
         * 左旋
         */
        public void SinistralRotation()
        {
            StartCoroutine(Rotation(Left));
        }

        IEnumerator Rotation(int direction)
        {
            var drum1 = Resources.Load<Sprite>("Drum/drum1");
            var drum2 = Resources.Load<Sprite>("Drum/drum2");
            gameObject.GetComponent<SpriteRenderer>().sprite = drum2;
            if (direction == Left)
            {
                drum.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                bool temp = slugs[0].activeInHierarchy;
                for (int i = 0; i < 5; i++)
                {
                    slugs[i].SetActive(slugs[i+1].activeInHierarchy);
                }
                slugs[5].SetActive(temp);
            }
            else if(direction == Right)
            {
                drum.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                bool temp = slugs[5].activeInHierarchy;
                for (int i = 5; i > 0; i--)
                {
                    slugs[i].SetActive(slugs[i-1].activeInHierarchy);
                }
                slugs[0].SetActive(temp);
            }

            yield return new WaitForSeconds(0.2f);
            
            gameObject.GetComponent<SpriteRenderer>().sprite = drum1;
            drum.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (direction == Left)
            {
                bool temp1 = slugs[5].activeInHierarchy;
                bool temp2 = slugs[4].activeInHierarchy;
                for (int i = 5; i > 1; i--)
                {
                    slugs[i].SetActive(slugs[i-2].activeInHierarchy);
                }
                slugs[1].SetActive(temp1);
                slugs[0].SetActive(temp2);
            }
            else if(direction == Right)
            {
                drum.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                bool temp1 = slugs[0].activeInHierarchy;
                bool temp2 = slugs[1].activeInHierarchy;
                for (int i = 0; i < 4; i++)
                {
                    slugs[i].SetActive(slugs[i+2].activeInHierarchy);
                }
                slugs[4].SetActive(temp1);
                slugs[5].SetActive(temp2);
            }
        }
    }
}