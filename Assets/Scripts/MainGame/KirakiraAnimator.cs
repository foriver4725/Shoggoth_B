using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace MainGame
{
    public class KirakiraAnimator : MonoBehaviour
    {
        [SerializeField, Header("きらきらのスプライト(最初の要素が初期)")] private Sprite[] kirakiraSprites;
        [SerializeField, Header("きらきらのImageコンポーネント")] private SpriteRenderer[] kirakiraComponents;
        [SerializeField, Range(0.1f, 5.0f), Header("スプライト遷移間隔(秒)")] private float interval;

        private void Start()
        {
            foreach (SpriteRenderer e in kirakiraComponents)
            {
                StartAnimation(e, destroyCancellationToken).Forget();
            }
        }

        private async UniTask StartAnimation(SpriteRenderer spriteRenderer, CancellationToken ct)
        {
            if (spriteRenderer == null) return;

            LoopedInt index = new(kirakiraSprites.Length);
            spriteRenderer.sprite = kirakiraSprites[index.Value];

            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: ct);
                index.Value++;
                spriteRenderer.sprite = kirakiraSprites[index.Value];
            }
        }
    }
    public struct LoopedInt
    {
        private int _value;

        /// <summary>
        /// Exclusive
        /// </summary>
        private readonly int _maxValue;

        public LoopedInt(int length, int initValue = 0)
        {
            _maxValue = (length is >= 2 and <= 20) ? length : 2;
            _value = (initValue >= 0 && initValue <= _maxValue) ? initValue : 0;
        }

        public int Value
        {
            get => _value;
            set
            {
                int val = value;
                while (val < 0) val += _maxValue;
                while (val >= _maxValue) val -= _maxValue;
                _value = val;
            }
        }
    }
}