using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currencyUI;

    public void Update()
    {
        currencyUI.text = "Currency: " + GameManager.instance.economy.currentCurrency;
    }
}
