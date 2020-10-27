using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

namespace Scripts.MapSystem
{
    [Serializable]
    public class MarkedObjectTargetData
    {
        [Range(10, 20)] public float showDistance;
        public AssetReference  assetRef;
        [HideInInspector] public Vector2 min;
        [HideInInspector] public Vector2 max;
        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public Camera mCamera;



    }
}