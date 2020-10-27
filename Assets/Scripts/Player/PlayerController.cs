using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerInputSystem))]
    public class PlayerController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private PlayerMoveSystem moveSystem;
        [SerializeField] private PlayerInputSystem inputSystem;
        [SerializeField] private Camera miniMapCamera;
#pragma warning restore 0649
        public Camera MiniMapCamera => miniMapCamera;

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