using UnityEngine;
using Scripts.MapSystem;

namespace Scripts.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private MiniMapController mapController;
        public MiniMapController MapController => mapController;
        
        public void Init(Transform playerTransform, Camera minimapCamera)
        {
            MapController.Init(playerTransform, minimapCamera);
        }
    }
}