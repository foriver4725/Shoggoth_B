using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IA;

namespace Scene
{
    public class OPtoCREDIT : MonoBehaviour
    {
        private void Update()
        {
            if (InputGetter.Instance.System_IsCredit)
            {
                SceneManager.LoadScene(SO_SceneName.Entity.Credit);
            }
        }
    }
}