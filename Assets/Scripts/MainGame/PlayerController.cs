using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public HPManager hpManager; // HP�Ǘ��X�N���v�g
    public float checkInterval = 0.1f; // �`�F�b�N�Ԋu

    void Start()
    {
        // ���Ԋu�œG�Ƃ̋������`�F�b�N����R���[�`�����J�n
        StartCoroutine(CheckDistanceToEnemies());
    }

    IEnumerator CheckDistanceToEnemies()
    {
        while (true)
        {
            // ���ׂĂ̓G���擾
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("shoggoth");
            foreach (GameObject enemy in enemies)
            {
                // �v���C���[�ƓG�̋������v�Z
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= 1f)
                {
                    // ������1�ȉ��̏ꍇ�AHP�����炷
                    hpManager.DecreaseHP();
                    // HP���������̂ŁA�����Ń��[�v�𔲂���
                    break;
                }
            }
            // �`�F�b�N�Ԋu��҂�
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
