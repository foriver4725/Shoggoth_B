using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class ItemPoints
    {
        private readonly ReadOnlyCollection<Transform>[] itemPoints = { null, null, null, null, null, null, null, null, };

        [SerializeField, Tooltip("B1F‚Ì1•”‰®–Ú")]
        private Transform[] itemPoints11;
        public ReadOnlyCollection<Transform> ItemPoints11
        {
            get
            {
                if (itemPoints[0] == null)
                {
                    itemPoints[0] = new ReadOnlyCollection<Transform>(itemPoints11);
                }
                return itemPoints[0];
            }
        }

        [SerializeField, Tooltip("B1F‚Ì2•”‰®–Ú")]
        private Transform[] itemPoints12;
        public ReadOnlyCollection<Transform> ItemPoints12
        {
            get
            {
                if (itemPoints[1] == null)
                {
                    itemPoints[1] = new ReadOnlyCollection<Transform>(itemPoints12);
                }
                return itemPoints[1];
            }
        }

        [SerializeField, Tooltip("B1F‚Ì3•”‰®–Ú")]
        private Transform[] itemPoints13;
        public ReadOnlyCollection<Transform> ItemPoints13
        {
            get
            {
                if (itemPoints[2] == null)
                {
                    itemPoints[2] = new ReadOnlyCollection<Transform>(itemPoints13);
                }
                return itemPoints[2];
            }
        }

        [SerializeField, Tooltip("B1F‚Ì4•”‰®–Ú")]
        private Transform[] itemPoints14;
        public ReadOnlyCollection<Transform> ItemPoints14
        {
            get
            {
                if (itemPoints[3] == null)
                {
                    itemPoints[3] = new ReadOnlyCollection<Transform>(itemPoints14);
                }
                return itemPoints[3];
            }
        }

        [SerializeField, Tooltip("B2F‚Ì1•”‰®–Ú")]
        private Transform[] itemPoints21;
        public ReadOnlyCollection<Transform> ItemPoints21
        {
            get
            {
                if (itemPoints[4] == null)
                {
                    itemPoints[4] = new ReadOnlyCollection<Transform>(itemPoints21);
                }
                return itemPoints[4];
            }
        }

        [SerializeField, Tooltip("B2F‚Ì2•”‰®–Ú")]
        private Transform[] itemPoints22;
        public ReadOnlyCollection<Transform> ItemPoints22
        {
            get
            {
                if (itemPoints[5] == null)
                {
                    itemPoints[5] = new ReadOnlyCollection<Transform>(itemPoints22);
                }
                return itemPoints[5];
            }
        }

        [SerializeField, Tooltip("B2F‚Ì3•”‰®–Ú")]
        private Transform[] itemPoints23;
        public ReadOnlyCollection<Transform> ItemPoints23
        {
            get
            {
                if (itemPoints[6] == null)
                {
                    itemPoints[6] = new ReadOnlyCollection<Transform>(itemPoints23);
                }
                return itemPoints[6];
            }
        }

        [SerializeField, Tooltip("B2F‚Ì4•”‰®–Ú")]
        private Transform[] itemPoints24;
        public ReadOnlyCollection<Transform> ItemPoints24
        {
            get
            {
                if (itemPoints[7] == null)
                {
                    itemPoints[7] = new ReadOnlyCollection<Transform>(itemPoints24);
                }
                return itemPoints[7];
            }
        }
    }
}
