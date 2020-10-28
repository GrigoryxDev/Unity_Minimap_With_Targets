using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private float refreshRate = 1f;
#pragma warning restore 0649
    private float timer;


    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            var fps = 1f / Time.unscaledDeltaTime;
            textMeshProUGUI.text = $"FPS: {fps:0}";
            timer = Time.unscaledTime + refreshRate;
        }
    }

}
