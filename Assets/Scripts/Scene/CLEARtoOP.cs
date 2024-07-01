using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class CLEARtoOP : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IA.InputGetter.Instance.System_IsSubmit)
            {
                Debug.Log(1);
                SceneManager.LoadSceneAsync(SO_SceneName.Entity.Title);
            }
        }
    }
}