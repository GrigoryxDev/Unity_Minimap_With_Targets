using UnityEngine;
using Scripts.MapSystem;

namespace Scripts.UI
{
    public class GameSceneUI : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private MiniMapController mapController;
#pragma warning restore 0649
        public MiniMapController MapController => mapController;

        public void Init(Transform playerTransform, Camera minimapCamera)
        {
            MapController.Init(playerTransform, minimapCamera);
        }
    }
}