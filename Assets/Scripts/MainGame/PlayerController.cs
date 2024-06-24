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

    void Update()
    {
        #region ���Ԋu�œG�Ƃ̋������`�F�b�N����

        // ���ׂĂ̓G���擾
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("shoggoth");
        foreach (GameObject enemy in enemies)
        {
            // �v���C���[�ƓG�̋������v�Z
            float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;
            if (sqrDistance <= 1.5f * 1.5f)
            {
                // ������1�ȉ��̏ꍇ�AHP�����炷
                if (!_isInvincible)
                {
                    _isInvincible = true;
                    hpManager.DecreaseHP();
                }
                // HP���������̂ŁA�����Ń��[�v�𔲂���
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
