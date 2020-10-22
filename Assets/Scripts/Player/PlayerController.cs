using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerInputSystem))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMoveSystem moveSystem;
        [SerializeField] private PlayerInputSystem inputSystem;

        private void Update()
        {
            if (inputSystem.AttackPressed)
            {
                Debug.Log("Attack!");
            }

            moveSystem.MoveOnUpdate(inputSystem.HorizontalInput, inputSystem.VerticalInput);
        }

        private void FixedUpdate()
        {
            moveSystem.OnFixedUpdate();
        }
    }
}