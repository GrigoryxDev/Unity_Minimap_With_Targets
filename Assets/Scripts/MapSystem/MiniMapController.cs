using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Scripts.MapSystem
{
    public class MiniMapController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private float circleOffset;
#pragma warning restore 0649
        private Camera mapCamera;
        private Transform player;
        private RectTransform rectTransform;
        private RectTransform Rect => rectTransform ?? (rectTransform = GetComponent<RectTransform>());
        private static Dictionary<int, MiniMapIcon> miniMapIcons = new Dictionary<int, MiniMapIcon>();
        private bool isInit;

        public void Init(Transform playerTransform, Camera minimapCamera)
        {
            mapCamera = minimapCamera;
            player = playerTransform;
            isInit = true;
        }

        private void LateUpdate()
        {
            if (isInit)
            {
                DrawIcons();
            }
        }

        public void AddObject(MarkedObject miniMapObject)
        {
            StartCoroutine(AdressableInst(miniMapObject));
        }

        private IEnumerator AdressableInst(MarkedObject miniMapObject)
        {
            var assetRef = miniMapObject.MMapData.assetRef;
            assetRef.LoadAssetAsync<GameObject>();
            var actionHandler = assetRef.InstantiateAsync(transform);
            yield return actionHandler;
            var minimapGObject = actionHandler.Result;
            var miniMapIcon = minimapGObject.GetComponent<MiniMapIcon>();
            miniMapIcon.Owner = miniMapObject;
            miniMapIcon.SetIcon(miniMapObject.MMapData.iconSprite, miniMapObject.MMapData.iconColor);
            miniMapIcon.ShowDistanceText(miniMapObject.MMapData.showDistance);

            var count = miniMapIcons.Count + 1;
            miniMapObject.MMapData.uiMMapIconIndex = count;

            miniMapIcons.Add(count, miniMapIcon);
        }

        public void RemoveObject(int index)
        {
            var objct = miniMapIcons[index];
            miniMapIcons.Remove(index);
            Destroy(objct.gameObject);
        }

        private void DrawIcons()
        {
            foreach (var minimapIcon in miniMapIcons)
            {
                var icon = minimapIcon.Value;
                if (icon.IsShowDistance)
                {
                    var distance = (int)Vector3.Distance(player.position, icon.Owner.transform.position);
                    minimapIcon.Value.SetText(distance.ToString());
                }

                var screenPos = (Vector2)mapCamera.WorldToViewportPoint(icon.Owner.transform.position);

                MathUtilities.ClampPositionToRectRectangle(Rect, ref screenPos);
                MathUtilities.ClampPositionToCircle(transform.position, (Rect.rect.width / 2) - circleOffset, ref screenPos);

                icon.transform.position = screenPos;
            }
        }

    }
}