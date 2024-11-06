using IA;
using UnityEngine;

namespace MainGame
{
    public sealed class OnDebugPlayerTeleport : MonoBehaviour
    {
        private void Update()
        {
            if (InputGetter.Instance.DebugAction1.Bool) transform.position = new Vector3(9, 106, -1);
            else if (InputGetter.Instance.DebugAction2.Bool) transform.position = new Vector3(35, 125, -1);
            else if (InputGetter.Instance.DebugAction3.Bool) transform.position = new Vector3(109, 11, -1);
            else if (InputGetter.Instance.DebugAction4.Bool) transform.position = new Vector3(126, 30, -1);
        }
    }
}
