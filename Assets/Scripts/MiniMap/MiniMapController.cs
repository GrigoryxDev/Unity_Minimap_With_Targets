﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MiniMap
{
    public class MiniMapController : MonoBehaviour
    {
        private Camera mapCamera;
        private Transform player;
        [SerializeField] private float circleOffset;
        private RectTransform rectTransform;
        private RectTransform Rect => rectTransform ?? (rectTransform = GetComponent<RectTransform>());
        private static List<MiniMapIcon> miniMapIcons = new List<MiniMapIcon>();
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

        public void AddObject(MiniMapObject miniMapObject)
        {
            var minimapGObject = Instantiate(miniMapObject.UiPrefab, transform);
            var miniMapIcon = minimapGObject.GetComponent<MiniMapIcon>();
            miniMapIcon.Owner = miniMapObject;
            miniMapIcon.SetIcon(miniMapObject.IconSprite, miniMapObject.Color);
            miniMapIcon.ShowDistanceText(miniMapObject.ShowDistance);

            miniMapIcons.Add(miniMapIcon);

        }

        public void RemoveObject(MiniMapIcon miniMapObject)
        {
            var objectIndex = miniMapIcons.BinarySearch(miniMapObject);
            var objct = miniMapIcons[objectIndex];
            miniMapIcons.Remove(objct);
            Destroy(objct.gameObject);

        }

        private void DrawIcons()
        {
            foreach (var minimapIcon in miniMapIcons)
            {
                if (minimapIcon.IsShowDistance)
                {
                    var distance = (int)Vector3.Distance(player.transform.position, minimapIcon.Owner.transform.position);
                    minimapIcon.SetText(distance.ToString());
                }


                var screenPos = (Vector2)mapCamera.WorldToViewportPoint(minimapIcon.Owner.transform.position);

                MathUtilities.ClampPositionToRectRectangle(Rect, ref screenPos);
                MathUtilities.ClampPositionToCircle(transform.position, (Rect.rect.width / 2) - circleOffset, ref screenPos);

                minimapIcon.transform.position = screenPos;
            }
        }

    }
}