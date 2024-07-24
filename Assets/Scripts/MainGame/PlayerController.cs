using Ex;
using MainGame;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (GameObject e in GameManager.Instance.Enemys)
        {
            // �v���C���[�ƓG�̋������v�Z
            float sqrDistance = ((Vector2)transform.position - (Vector2)e.transform.position).sqrMagnitude;
            if (sqrDistance <= SO_Player.Entity.PlayerDamageRange * SO_Player.Entity.PlayerDamageRange)
            {
                // ������1�ȉ��̏ꍇ�AHP�����炷
                if (GameManager.Instance.CurrentHP > 0 && !_isInvincible)
                {
                    // �N���A�܂��̓Q�[���I�[�o�[�Ȃ��e���Ȃ�
                    if (GameManager.Instance.IsClear || GameManager.Instance.IsOver) continue;

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
