using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerInputSystem : MonoBehaviour
    {
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public bool AttackPressed { get; set; }
        private void Update()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
            AttackPressed = Input.GetKeyDown(KeyCode.Space);
        }
    }
}