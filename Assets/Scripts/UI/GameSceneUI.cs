using UnityEngine;
using Scripts.MapSystem;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Scripts.UI
{
    public class GameSceneUI : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private MiniMapController mapController;
        [SerializeField] private Transform targetsGroup;
        [SerializeField] private Button spawnButton;
#pragma warning restore 0649
        public MiniMapController MapController => mapController;
        public Transform TargetsGroup => targetsGroup;

        public void Init(Transform playerTransform, Camera minimapCamera, UnityAction action)
        {
            MapController.Init(playerTransform, minimapCamera);
            spawnButton.onClick.AddListener(action);
        }
    }
}