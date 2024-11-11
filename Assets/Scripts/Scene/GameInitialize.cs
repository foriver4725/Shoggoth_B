using General;
using UnityEngine;

namespace Scene
{
    public static class GameInitialize
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            SaveDataHolder.Instance.Load();
        }
    }
}
