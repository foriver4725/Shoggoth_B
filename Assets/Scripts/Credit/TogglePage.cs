using IA;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Credit
{
    public class TogglePage : MonoBehaviour
    {
        [SerializeField] private Image _page1;
        [SerializeField] private Image _page2;

        void Start()
        {
            _page1.enabled = true;
            _page2.enabled = false;
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsCancel)
            {
                SceneManager.LoadScene(SO_SceneName.Entity.Title);
            }

            if (InputGetter.Instance.System_IsSubmit)
            {
                _page1.enabled = !_page1.enabled;
                _page2.enabled = !_page2.enabled;
            }
        }
    }
}
