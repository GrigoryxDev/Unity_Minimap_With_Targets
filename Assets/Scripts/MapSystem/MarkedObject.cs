using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Scripts.UI;

namespace Scripts.MapSystem
{
    public class MarkedObject : MonoBehaviour, ISpawned
    {
        private enum States { OnInit, Show, Hide }
        [SerializeField] private MarkedObjectMMapData mmapData;
        [SerializeField] private MarkedObjectTargetData targetData;
        private States state;
        public TargetUI TargetUi { get; private set; }
        public MarkedObjectMMapData MMapData => mmapData;
        public MarkedObjectTargetData TargetData => targetData;

        public void Init()
        {
            state = States.OnInit;

            var gsm = GameSceneManager.Instance;
            gsm.GameSceneUI.MapController.AddObject(this);

            TargetData.playerTransform = gsm.Player.transform;
            TargetData.mCamera = Camera.main;

            var targetUiGO = Instantiate(targetData.uiPrefab, gsm.GameSceneUI.transform);
            TargetUi = targetUiGO.GetComponent<TargetUI>();
            TargetUi.Init();
            TargetUi.Owner = transform;


            TargetData.min = new Vector2(TargetUi.TargetUiImg.GetPixelAdjustedRect().width / 2, TargetUi.TargetUiImg.GetPixelAdjustedRect().height / 2);
            TargetData.max = new Vector2(Screen.width - TargetData.min.x, Screen.height - TargetData.min.y);

            gsm.ChangeObjectPosition(transform);
            state = States.Show;
        }

        public void OnObjDestroy()
        {
            TargetUi.OnObjDestroy();
            Destroy(gameObject);
        }

        private void LateUpdate()
        {
            var distance = Vector3.Distance(transform.position, TargetData.playerTransform.position);

            state = distance <= TargetData.showDistance ? States.Hide : States.Show;

            switch (state)
            {
                case States.Show:
                    ShowTarget(true);
                    Vector2 pos = TargetData.mCamera.WorldToScreenPoint(transform.position);

                    // Check if the target is behind us and place it to the top if true
                    if (Vector3.Dot(transform.position - TargetData.playerTransform.position, TargetData.playerTransform.forward) < 0)
                    {
                        pos.y = TargetData.max.y;
                    }
                    pos.x = Mathf.Clamp(pos.x, TargetData.min.x, TargetData.max.x);
                    pos.y = Mathf.Clamp(pos.y, TargetData.min.y, TargetData.max.y);

                    TargetUi.DistanceMeterTMP.transform.position = pos;
                    TargetUi.DistanceMeterTMP.text = distance.ToString("#####");
                    break;
                case States.Hide:
                    ShowTarget(false);
                    break;
            }
        }

        private void ShowTarget(bool show)
        {
            TargetUi.TargetUiImg.enabled = show;
            TargetUi.DistanceMeterTMP.enabled = show;
        }

    }
}