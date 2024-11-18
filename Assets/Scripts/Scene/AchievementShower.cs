using Cysharp.Threading.Tasks;
using General;
using Sirenix.OdinInspector;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
    public sealed class AchievementShower : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Images images;

        private void OnEnable() => SetEnabledFromSaveData(destroyCancellationToken).Forget();

        private async UniTaskVoid SetEnabledFromSaveData(CancellationToken ct)
        {
            await UniTask.NextFrame(ct);
            images.SetEnabledFromSaveData(SaveDataHolder.Instance.SaveData);
        }

        [Serializable]
        private sealed class Images
        {
            [SerializeField, Required, SceneObjectsOnly]
            private Image difficultyEasyImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image difficultyNormalImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image difficultyHardImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image difficultyNightmareImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image clearNum1Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image clearNum10Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image clearNum50Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image clearNum100Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image overNum1Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image overNum10Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image overNum50Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image overNum100Image;

            [SerializeField, Required, SceneObjectsOnly]
            private Image toiletEnteredImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image toiletExplodedImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image chasedALotImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image notChasedImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image withoutAnyHealImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image withAllHealImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image breakAquaGlassImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image stepALotImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image secretItemImage;

            [SerializeField, Required, SceneObjectsOnly]
            private Image achievedAllImage;

            public void SetEnabledFromSaveData(SaveData saveData)
            {
                if (saveData is null) return;

                difficultyEasyImage.transform.parent.gameObject.SetActive(saveData.HasEasyCleared);
                difficultyNormalImage.transform.parent.gameObject.SetActive(saveData.HasNormalCleared);
                difficultyHardImage.transform.parent.gameObject.SetActive(saveData.HasHardCleared);
                difficultyNightmareImage.transform.parent.gameObject.SetActive(saveData.HasNightmareCleared);

                clearNum1Image.transform.parent.gameObject.SetActive(saveData.ClearNum >= 1);
                clearNum10Image.transform.parent.gameObject.SetActive(saveData.ClearNum >= 10);
                clearNum50Image.transform.parent.gameObject.SetActive(saveData.ClearNum >= 50);
                clearNum100Image.transform.parent.gameObject.SetActive(saveData.ClearNum >= 100);

                overNum1Image.transform.parent.gameObject.SetActive(saveData.OverNum >= 1);
                overNum10Image.transform.parent.gameObject.SetActive(saveData.OverNum >= 10);
                overNum50Image.transform.parent.gameObject.SetActive(saveData.OverNum >= 50);
                overNum100Image.transform.parent.gameObject.SetActive(saveData.OverNum >= 100);

                toiletEnteredImage.transform.parent.gameObject.SetActive(saveData.HasEnteredToilet);
                toiletExplodedImage.transform.parent.gameObject.SetActive(saveData.HasToiletExploded);

                chasedALotImage.transform.parent.gameObject.SetActive(saveData.HasBeenChasedALot);
                notChasedImage.transform.parent.gameObject.SetActive(saveData.HasClearedWithoutChasing);

                withoutAnyHealImage.transform.parent.gameObject.SetActive(saveData.HasClearedWithoutAnyHeal);
                withAllHealImage.transform.parent.gameObject.SetActive(saveData.HasClearedWithAllHeal);

                breakAquaGlassImage.transform.parent.gameObject.SetActive(saveData.HasBrokenAquaGlass);
                stepALotImage.transform.parent.gameObject.SetActive(saveData.HasSteppedALot);

                secretItemImage.transform.parent.gameObject.SetActive(saveData.HasFoundSecretItem);
                achievedAllImage.transform.parent.gameObject.SetActive(SaveData.HasAchievedAll(saveData));
            }
        }
    }
}
