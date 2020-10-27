using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Scripts.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Scripts.MapSystem
{
    public class MarkedObject : MonoBehaviour, ISpawned, ISpawner
    {
        private enum States { OnInit, Show, Hide }
#pragma warning disable 0649
        [SerializeField] private MarkedObjectMMapData mmapData;
        [SerializeField] private MarkedObjectTargetData targetData;
#pragma warning restore 0649
        private States state;
        public TargetUI TargetUi { get; private set; }
        public MarkedObjectMMapData MMapData => mmapData;

        public MarkedObjectTargetData TargetData => targetData;
        private float distance;
        public void Init()
        {
            state = States.OnInit;
            var gsm = GameSceneManager.Instance;

            TargetData.playerTransform = gsm.Player.transform;
            TargetData.mCamera = Camera.main;

            gsm.ChangeObjectPosition(transform);

            gsm.AdressableInstantiate.AdressableInst(targetData.assetRef, gsm.GameSceneUI.TargetsGroup, InitAfterInstantiate);

            gsm.GameSceneUI.MapController.AddObject(this);

        }

        public void InitAfterInstantiate(AsyncOperationHandle<GameObject> obj)
        {
            var targetUiGO = obj.Result;
            TargetUi = targetUiGO.GetComponent<TargetUI>();
            TargetUi.Init();
            TargetUi.Owner = transform;
            TargetData.min = new Vector2(TargetUi.TargetUiImg.GetPixelAdjustedRect().width / 2, TargetUi.TargetUiImg.GetPixelAdjustedRect().height / 2);
            TargetData.max = new Vector2(Screen.width - TargetData.min.x, Screen.height - TargetData.min.y);

            state = States.Show;
        }

        public void OnObjDestroy()
        {
            GameSceneManager.Instance.GameSceneUI.MapController.RemoveObject(mmapData.uiMMapIconIndex);
            TargetUi.OnObjDestroy();
            Destroy(gameObject);
        }

        private void LateUpdate()
        {

            if (state != States.OnInit)
            {
                distance = Vector3.Distance(transform.position, TargetData.playerTransform.position);
                state = distance <= TargetData.showDistance ? States.Hide : States.Show;

            }

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