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
            if (InputGetter.Instance.MainGame_IsTalk)
            {
                GameManager.Instance.Player.transform.position = new(100, 36, -1);
            }
            if (InputGetter.Instance.MainGame_IsGimmick)
            {
                GameManager.Instance.Player.transform.position = new(0, 136, -1);
            }
        }



        [SerializeField] GameObject tmpObj;
        [SerializeField] Sprite tmpSprite;
        void __ForAllgameObjects()
        {
            GameObject[] allObjs = FindObjectsOfType<GameObject>();
            foreach (GameObject targetObj in allObjs)
            {
                if (targetObj.name == "Object(Clone)")
                {
                    targetObj.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
