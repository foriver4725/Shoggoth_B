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
    static public TextMeshProUGUI textMeshProUGUI;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    static public void ShoggothLook()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[0];
    }
    static public void ShoggothEscape()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[1];
    }
    static public void ShoggothDamage()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[2];
    }
    static public void MapMove1F()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[0];
    }
    static public void MapMoveB1F()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[1];
    }
    static public void MapMoveB2F()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[2];
    }
    static public void EscapeIndex()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[0];
    }
    static public void EscapeIndex2()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[1];
    }
    static public void EscapeIndex3()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[2];
    }
    static public void EscapeIndex4()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[3];
    }
    static public void LockDoor()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[4];
    }
    static public void BreakDoor()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[5];
    }
    static public void IndexShoggoth()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[8];
    }
    static public void IndexShoggoth2()
    {
        textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[9];
    }
}