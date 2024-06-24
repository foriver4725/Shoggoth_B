using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP管理スクリプト
    public float checkInterval = 0.1f; // チェック間隔

    void Start()
    {
        // 一定間隔で敵との距離をチェックするコルーチンを開始
        StartCoroutine(CheckDistanceToEnemies());
    }

    IEnumerator CheckDistanceToEnemies()
    {
        while (true)
        {
            // すべての敵を取得
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("shoggoth");
            foreach (GameObject enemy in enemies)
            {
                // プレイヤーと敵の距離を計算
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= 1f)
                {
                    // 距離が1以下の場合、HPを減らす
                    hpManager.DecreaseHP();
                    // HPが減ったので、ここでループを抜ける
                    break;
                }
            }
            // チェック間隔を待つ
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
