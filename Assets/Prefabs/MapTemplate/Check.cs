using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Check : MonoBehaviour
    {
        GameObject[] path;
        GameObject[] block;

        GameObject[] stokingPoint;

        void Start()
        {
            path = GameObject.FindGameObjectsWithTag("path");
            foreach (GameObject e in path)
            {
                e.GetComponent<SpriteRenderer>().color = Color.green;
            }

            block = GameObject.FindGameObjectsWithTag("block");
            foreach (GameObject e in block)
            {
                e.GetComponent<SpriteRenderer>().color = Color.black;
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
                    obj.GetComponent<SpriteRenderer>().color = e.ThemeColor;
                }
            }

            foreach (TagSprite e in SO_TileSprite.Entity.Walls)
            {
                GameObject[] objArr = GameObject.FindGameObjectsWithTag(e.TagName);
                foreach (GameObject obj in objArr)
                {
                    obj.GetComponent<SpriteRenderer>().color = e.ThemeColor;
                }
            }
        }

        void Update()
        {

        }
    }
}
