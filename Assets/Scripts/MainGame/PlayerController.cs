using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP管理スクリプト
    private float _invincibleCount = 0f;
    private bool _isInvincible = false;

    void Update()
    {
        #region 一定間隔で敵との距離をチェックする

        // すべての敵を取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("shoggoth");
        foreach (GameObject enemy in enemies)
        {
            // プレイヤーと敵の距離を計算
            float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;
            if (sqrDistance <= 1.5f * 1.5f)
            {
                // 距離が1以下の場合、HPを減らす
                if (!_isInvincible)
                {
                    _isInvincible = true;
                    hpManager.DecreaseHP();
                }
                // HPが減ったので、ここでループを抜ける
                break;
            }
        }

        if (_isInvincible)
        {
            _invincibleCount += Time.deltaTime;
            if (_invincibleCount > SO_Player.Entity.InvincibleTime)
            {
                _invincibleCount = 0f;
                _isInvincible = false;
            }
        }

        #endregion
    }
}
