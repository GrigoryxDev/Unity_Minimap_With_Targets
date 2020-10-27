using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TargetUI : MonoBehaviour, ISpawned
    {
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI meter;
        [SerializeField] private GameObject activationText;
        [SerializeField] private Image targetUiImg;
#pragma warning restore 0649
        public Image TargetUiImg => targetUiImg;
        public TextMeshProUGUI DistanceMeterTMP => meter;
        public Transform Owner { get; set; }

        public void Init()
        {
            ShowActiveTex(false);
        }

        public void OnObjDestroy()
        {
            Destroy(gameObject);
        }

        public void ShowActiveTex(bool show)
        {
            activationText.SetActive(show);
        }
    }
}