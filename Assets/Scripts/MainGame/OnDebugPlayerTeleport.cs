using IA;
using UnityEngine;

namespace MainGame
{
    public sealed class OnDebugPlayerTeleport : MonoBehaviour
    {
        private void Update()
        {
            if (InputGetter.Instance.DebugAction1.Bool) transform.position = new Vector3(0, 0, 0);
            else if (InputGetter.Instance.DebugAction2.Bool) transform.position = new Vector3(0, 0, 0);
            else if (InputGetter.Instance.DebugAction3.Bool) transform.position = new Vector3(0, 0, 0);
            else if (InputGetter.Instance.DebugAction4.Bool) transform.position = new Vector3(0, 0, 0);
            else if (InputGetter.Instance.DebugAction5.Bool) transform.position = new Vector3(0, 0, 0);
        }
    }
}
