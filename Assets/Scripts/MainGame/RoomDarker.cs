using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MainGame
{
    public sealed class RoomDarker : MonoBehaviour
    {
        [SerializeField] private Light2D sceneLight;
        private void Start() => sceneLight.intensity = 0;
    }
}
