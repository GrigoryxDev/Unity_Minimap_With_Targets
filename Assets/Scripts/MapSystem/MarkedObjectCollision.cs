using Characters.Player;
using UnityEngine;

namespace Scripts.MapSystem
{
    public class MarkedObjectCollision : MonoBehaviour
    {
        private MarkedObject markedObject;
        private MarkedObject MarkedObject => markedObject ?? (markedObject = GetComponent<MarkedObject>());
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                MarkedObject.TargetUi.ShowActiveTex(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                MarkedObject.TargetUi.ShowActiveTex(false);
            }
        }
    }
}