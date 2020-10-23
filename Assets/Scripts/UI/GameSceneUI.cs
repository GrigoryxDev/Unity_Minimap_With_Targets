using UnityEngine;
using Scripts.MiniMap;

namespace Scripts.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private MiniMapController mapController;
        [SerializeField] private TargetUI targetUI;
        [SerializeField] private GameObject activeText;
        public MiniMapController MapController => mapController;
        public void Init(Transform playerTransform, Camera minimapCamera)
        {
            MapController.Init(playerTransform, minimapCamera);
            targetUI.Init(playerTransform);

        }

        public void ShowActiveTex(bool show)
        {
            activeText.SetActive(show);
        }
    }
}