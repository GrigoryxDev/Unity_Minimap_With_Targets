using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TargetUI : MonoBehaviour
    {
        private enum States { OnInit, Show, Hide }
        [SerializeField, Range(10, 20)] private float showDistance;
        [SerializeField] private TextMeshProUGUI meter;
        [SerializeField] private GameObject targetUIObject;
        [SerializeField] private Transform target;
        [SerializeField] private Image targetUiImg;

        private Vector2 min;
        private Vector2 max;
        private Transform playerTransform;
        private Camera mCamera;
        private States state;

        public void Init(Transform playerTransform)
        {
            state = States.OnInit;
            this.playerTransform = playerTransform;
            mCamera = Camera.main;
            min = new Vector2(targetUiImg.GetPixelAdjustedRect().width / 2, targetUiImg.GetPixelAdjustedRect().height / 2);
            max = new Vector2(Screen.width - min.x, Screen.height - min.y);
            state = States.Show;

        }

        private void LateUpdate()
        {
            var distance = Vector3.Distance(target.position, playerTransform.position);

            if (distance <= showDistance)
            {
                state = States.Hide;
            }
            else
            {
                state = States.Show;
            }

            switch (state)
            {
                case States.OnInit:
                    break;
                case States.Show:
                    ShowTarget(true);
                    Vector2 pos = mCamera.WorldToScreenPoint(target.position);

                    // Check if the target is behind us, to only show the icon once the target is in front
                    if (Vector3.Dot(target.position - playerTransform.position, playerTransform.forward) < 0)
                    {
                        pos.y =max.y;
                        // Check if the target is on the left side of the screen
                        if (pos.x < Screen.width / 2)
                        {
                            // Place it on the right (Since it's behind the player, it's the opposite)
                            pos.x = max.x;
                        }
                        else
                        {
                            // Place it on the left side
                            pos.x = min.x;
                        }
                    }

                    // Limit the X and Y positions
                    pos.x = Mathf.Clamp(pos.x, min.x, max.x);
                    pos.y = Mathf.Clamp(pos.y, min.y, max.y);


                    targetUIObject.transform.position = pos;

                    meter.text = distance.ToString("#####");
                    break;
                case States.Hide:
                    ShowTarget(false);
                    break;
            }
        }

        private void ShowTarget(bool show)
        {
            targetUiImg.enabled = show;
            meter.enabled = show;
        }
    }
}