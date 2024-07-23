using Ex;
using MainGame;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP�Ǘ��X�N���v�g
    private float _invincibleCount = 0f;
    private bool _isInvincible = false;

    [SerializeField] AudioSource damagedAS;

    void Update()
    {
        #region ���Ԋu�œG�Ƃ̋������`�F�b�N����

        // ���ׂĂ̓G���擾
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("shoggoth");
        foreach (GameObject enemy in enemies)
        {
            // �v���C���[�ƓG�̋������v�Z
            float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;
            if (sqrDistance <= SO_Player.Entity.PlayerDamageRange * SO_Player.Entity.PlayerDamageRange)
            {
                // ������1�ȉ��̏ꍇ�AHP�����炷
                if (GameManager.Instance.CurrentHP > 0 && !_isInvincible)
                {
                    _isInvincible = true;
                    damagedAS.Raise(SO_Sound.Entity.DamageTookSE, SType.SE);
                    hpManager.DecreaseHP();
                }
                // HP���������̂ŁA�����Ń��[�v�𔲂���
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
