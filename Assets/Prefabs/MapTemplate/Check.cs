using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Check : MonoBehaviour
    {
        GameObject[] path;
        GameObject[] block;

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
        }

        void Update()
        {

        }
    }
}
