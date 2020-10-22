using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.UI;
using Characters.Player;

namespace Scripts.MiniMap
{
    public class MiniMapObject : MonoBehaviour
    {
        [SerializeField] private GameObject uiPrefab;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private Color iconColor;
        [SerializeField] private bool showDistance;
        public bool ShowDistance => showDistance;
        public GameObject UiPrefab => uiPrefab;
        public Sprite IconSprite => iconSprite;
        public Color Color => iconColor;

        private void Start()
        {
            var gsm = GameSceneManager.Instance;
            gsm.GameSceneUI.MapController.AddObject(this);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                GameSceneManager.Instance.GameSceneUI.ShowActiveTex(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                GameSceneManager.Instance.GameSceneUI.ShowActiveTex(false);
            }
        }
    }
}