using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI nameContent;

    public void Initialize(string name, Action callback)
    {
        nameContent.text = name;
        buyBtn.onClick.AddListener(() => callback());
    }
}

