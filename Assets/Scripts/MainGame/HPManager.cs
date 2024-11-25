using MainGame;
using Sirenix.OdinInspector;
using SO;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField, Required, SceneObjectsOnly, Tooltip("左のハートから順に")]
    private Image[] heartImages;

    [SerializeField]
    private Image DamageImg;

    void Start()
    {
        GameManager.Instance.CurrentHP = SO_Player.Entity.PlayerInitHp;
        UpdateHeartImages(GameManager.Instance.CurrentHP);
    }

    void Update()
    {
        UpdateHeartImages(GameManager.Instance.CurrentHP);
        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);
    }

    public void DecreaseHP()
    {
        if (GameManager.Instance.CurrentHP <= 0) return;
        GameManager.Instance.CurrentHP--;
        DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
    }

    private void UpdateHeartImages(int hp)
    {
        int len = heartImages.Length;
        for (int i = 0; i < len; i++)
        {
            heartImages[i].enabled = i < hp;
        }
    }
}
