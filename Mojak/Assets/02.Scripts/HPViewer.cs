using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = playerController.CurrentHP / playerController.MaxHP;
    }
}
