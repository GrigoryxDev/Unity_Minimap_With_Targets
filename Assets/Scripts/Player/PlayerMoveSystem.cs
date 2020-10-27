using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveSystem : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private float turnSmoothTime = .1f;
        [SerializeField] private Transform cam;
        [SerializeField, Range(0f, 25f)] private float moveSpeed;
        [SerializeField] private float gravityValue = -9.81f;
#pragma warning restore 0649
        private float turnSmoothVelocity;

        private CharacterController characterController;
        private CharacterController CharacterController => characterController ?? (characterController = GetComponent<CharacterController>());

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

        public void MoveOnUpdate(float horiz, float vert)
        {
            var direction = new Vector3(horiz, 0, vert).normalized;

            if (direction.magnitude >= 0.1)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0, angle, 0);

                var moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

                CharacterController.Move(moveDirection.normalized * MoveSpeed * Time.deltaTime);
            }
        }

        public void OnFixedUpdate()
        {
            Vector3 gravity = new Vector3(0f, gravityValue, 0f);
            CharacterController.Move(gravity * Time.fixedDeltaTime);
        }
    }
}