using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MiniMap
{
    public class MiniMapIcon : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textDistance;
        public MiniMapObject Owner { get; set; }
        public bool IsShowDistance { get; private set; }

        public void ShowDistanceText(bool hide)
        {
            textDistance.enabled = hide;
            IsShowDistance = hide;
        }

        public void SetIcon(Sprite icon, Color color)
        {
            image.color = color;
            image.sprite = icon;
        }

        public void SetText(string text)
        {
            textDistance.text = text;
        }
    }
}