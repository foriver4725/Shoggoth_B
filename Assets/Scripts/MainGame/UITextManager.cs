using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;
using IA;
using TMPro;
using static UnityEngine.Mesh;

public class UITextManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public SO_UIConsoleText uiConsoleText;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        uiConsoleText = Resources.Load<SO_UIConsoleText>("SO_UIConsoleText");
    }


    // Update is called once per frame
    void Update()
    {

        textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[0];
            
        
        
    }
}
