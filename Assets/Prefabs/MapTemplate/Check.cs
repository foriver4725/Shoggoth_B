using IA;
using MainGame;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Check : MonoBehaviour
    {
#if false
        GameObject[] stokingPoint;

        void Start()
        {
            foreach (TagSprite e in SO_TileSprite.Entity.Debugs)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.GetComponent<SpriteRenderer>().sprite = e.Sprite;
                }
            }

            stokingPoint = GameObject.FindGameObjectsWithTag("type_stokingpoint");
            foreach (GameObject e in stokingPoint)
            {
                e.GetComponent<SpriteRenderer>().color = Color.red;
            }

            foreach (TagSprite e in SO_TileSprite.Entity.Floors)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.transform.parent.GetComponent<SpriteRenderer>().sprite = e.Sprite;
                }
            }

            foreach (TagSprite e in SO_TileSprite.Entity.Walls)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.transform.parent.GetComponent<SpriteRenderer>().sprite = e.Sprite;
                }
            }

            foreach (TagSprite e in SO_TileSprite.Entity.Objects)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = e.Sprite;
                }
            }

            foreach (TagSprites e in SO_TileSprite.Entity.RandomObjects)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = e.Sprites[Random.Range(0, e.Sprites.Count)];
                }
            }

            __ForAllgameObjects();
        }

        void Update()
        {

        }



        [SerializeField] GameObject tmpObj;
        [SerializeField] Sprite tmpSprite;
        void __ForAllgameObjects()
        {
            //GameObject[] allObjs = FindObjectsOfType<GameObject>();
            //foreach (GameObject targetObj in allObjs)
            //{
            //    if (targetObj.name == "Object(Clone)")
            //    {
            //        targetObj.GetComponent<SpriteRenderer>().enabled = false;
            //    }
            //}
        }

        [SerializeField] private Sprite tmp;
        private void Start()
        {
            foreach (var e in GameObject.FindGameObjectsWithTag("path"))
            {
                e.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (var e in GameObject.FindGameObjectsWithTag("block"))
            {
                e.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }

            foreach (var e in GameObject.FindGameObjectsWithTag("type_stokingpoint"))
            {
                e.GetComponent<SpriteRenderer>().color = Color.red;
                e.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (var e in GameObject.FindGameObjectsWithTag("type_stokingpoint_1"))
            {
                e.GetComponent<SpriteRenderer>().color = Color.blue;
                e.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (var e in GameObject.FindGameObjectsWithTag("type_stokingpoint_2"))
            {
                e.GetComponent<SpriteRenderer>().color = Color.green;
                e.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
#endif

#if false
        private void Start()
        {
            GameObject[] objs = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                foreach (TagSprite e in SO_TileSprite.Entity.Floors)
                {
                    if (obj.tag == e.TagName)
                    {
                        obj.transform.parent.GetComponent<SpriteRenderer>().sprite = e.Sprite;
                    }
                }
            }
        }
#endif
    }
}
