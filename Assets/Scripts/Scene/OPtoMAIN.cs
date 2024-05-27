using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OPtoMAIN : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IA.InputGetter.Instance.Title_Istart)
        {
            SceneManager.LoadSceneAsync("Main");
        }
    }
}
