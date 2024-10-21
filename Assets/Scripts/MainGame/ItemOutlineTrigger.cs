using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    [Serializable]
    public sealed class ItemOutlineTrigger
    {
        [SerializeField, Tooltip("0番目のアイテム")]
        private Image itemImage0;

        [SerializeField, Tooltip("1番目のアイテム")]
        private Image itemImage1;

        [SerializeField, Tooltip("2番目のアイテム")]
        private Image itemImage2;

        [SerializeField, Tooltip("3番目のアイテム")]
        private Image itemImage3;

        /// <summary>
        /// 0が1F、1がB1F、2がB2F
        /// </summary>
        public void SetActivation(int floor)
        {
            if (floor is not (0 or 1 or 2)) return;

            if (floor == 0)
            {
                itemImage0.gameObject.SetActive(false);
                itemImage1.gameObject.SetActive(false);
                itemImage2.gameObject.SetActive(false);
                itemImage3.gameObject.SetActive(false);
            }
            else if (floor == 1)
            {
                itemImage0.gameObject.SetActive(false);
                itemImage1.gameObject.SetActive(false);
                itemImage2.gameObject.SetActive(true);
                itemImage3.gameObject.SetActive(true);
            }
            else if (floor == 2)
            {
                itemImage0.gameObject.SetActive(true);
                itemImage1.gameObject.SetActive(true);
                itemImage2.gameObject.SetActive(false);
                itemImage3.gameObject.SetActive(false);
            }
            else throw new();
        }
    }
}
