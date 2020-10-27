using System;
using UnityEngine;

namespace Scripts.MapSystem
{
    [Serializable]
    public struct MarkedObjectMMapData
    {
        public GameObject uiPrefab;
        public Sprite iconSprite;
        public Color iconColor;
        public bool showDistance;
    }
}