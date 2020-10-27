using Scripts.MapSystem;
using UnityEngine;

namespace Scripts.UI
{
    public class ActivationTextInput : MonoBehaviour
    {
        private TargetUI target;
        private TargetUI Target => target ?? (target = GetComponentInParent<TargetUI>());

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameSceneManager.Instance.ChangeObjectPosition(Target.Owner);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Target.Owner.GetComponent<MarkedObject>().OnObjDestroy();
            }


        }
    }
}