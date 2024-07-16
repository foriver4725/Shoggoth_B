using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class OPtoMAIN : MonoBehaviour
    {
        [SerializeField] private Image _startDescription;

        // Start is called before the first frame update
        void Start()
        {
            _startDescription.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (IA.InputGetter.Instance.System_IsSubmit)
            {
                if (!_startDescription.enabled)
                {
                    _startDescription.enabled = true;
                }
                else
                {
                    SceneManager.LoadSceneAsync(SO_SceneName.Entity.MainGame);
                }
            }
        }
    }
}