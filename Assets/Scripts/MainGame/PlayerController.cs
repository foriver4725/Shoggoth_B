using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shoggoth")
        {
            hpManager.DecreaseHP();
        }
    }
}
