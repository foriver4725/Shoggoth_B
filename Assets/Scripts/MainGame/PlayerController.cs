using Ex;
using MainGame;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP管理スクリプト
    private float _invincibleCount = 0f;
    private bool _isInvincible = false;

    [SerializeField] AudioSource damagedAS;

    void Update()
    {
        #region 一定間隔で敵との距離をチェックする

        // すべての敵を取得
        foreach (GameObject e in GameManager.Instance.Enemys)
        {
            // プレイヤーと敵の距離を計算
            float sqrDistance = ((Vector2)transform.position - (Vector2)e.transform.position).sqrMagnitude;
            if (sqrDistance <= SO_Player.Entity.PlayerDamageRange * SO_Player.Entity.PlayerDamageRange)
            {
                // 距離が1以下の場合、HPを減らす
                if (GameManager.Instance.CurrentHP > 0 && !_isInvincible)
                {
                    // クリアまたはゲームオーバーなら被弾しない
                    if (GameManager.Instance.IsClear || GameManager.Instance.IsOver) continue;

                    _isInvincible = true;
                    damagedAS.Raise(SO_Sound.Entity.DamageTookSE, SType.SE);
                    hpManager.DecreaseHP();
                }
                // HPが減ったので、ここでループを抜ける
                break;
            }
        }

        if (_isInvincible)
        {
            _invincibleCount += Time.deltaTime;
            if (_invincibleCount > (SO_Debug.Entity.IsInvincible ? 10000 : SO_Player.Entity.InvincibleTime))
            {
                _invincibleCount = 0f;
                _isInvincible = false;
            }
        }

        #endregion
    }
}
