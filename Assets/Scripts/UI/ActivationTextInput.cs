using UnityEngine;

namespace Scripts.UI
{
    public class ActivationTextInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameSceneManager.Instance.ChangeObjectPosition();
            }
        }
    }
}