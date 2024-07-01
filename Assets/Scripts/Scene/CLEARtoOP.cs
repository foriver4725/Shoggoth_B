using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class CLEARtoOP : MonoBehaviour
    {
        private bool _isClickable = false;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CountClickTime());
        }

        // Update is called once per frame
        void Update()
        {
            if (_isClickable && IA.InputGetter.Instance.System_IsSubmit)
            {
                SceneManager.LoadSceneAsync(SO_SceneName.Entity.Title);
            }
        }

        IEnumerator CountClickTime()
        {
            yield return new WaitForSeconds(5);
            _isClickable = true;
        }
    }
}