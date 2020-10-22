using UnityEngine;
using Scripts.MiniMap;

namespace Scripts.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private MiniMapController mapController;
        [SerializeField] private GameObject activeText;
        public MiniMapController MapController => mapController;


        public void ShowActiveTex(bool show)
        {
            activeText.SetActive(show);
        }
    }
}