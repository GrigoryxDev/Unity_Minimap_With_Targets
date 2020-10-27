using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.MapSystem
{
    [Serializable]
    public struct MarkedObjectMMapData
    {
        public AssetReference assetRef;
        public Sprite iconSprite;
        public Color iconColor;
        public bool showDistance;
    }
}