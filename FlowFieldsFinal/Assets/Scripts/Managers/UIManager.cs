using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currencyUI;
    public TextMeshProUGUI waveAmountUI;
    public GameObject towerUI;
    public TextMeshProUGUI switchPriority;
    public bool towerUIEnabled;
    public TowerAgent currentTower;

    public void Update()
    {
        currencyUI.text = "Currency: " + GameManager.instance.economy.currentCurrency;
        waveAmountUI.text = "Wave: " + GameManager.instance.waveManager.currentWave;

        if (Input.GetKeyDown(KeyCode.Escape) && towerUIEnabled)
        {
            CloseTower();
        }
    }

    public void TowerUI(TowerAgent tower)
    {
        if (!towerUIEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            currentTower = tower;
            switchPriority.text = "Priority: " + currentTower.targetPriority;
            towerUIEnabled = true;
            towerUI.SetActive(true);
        }
    }

    private void CloseTower()
    {
        if (towerUIEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            towerUIEnabled = false;
            towerUI.SetActive(false);
        }
    }

    public void UpdateTowerUI()
    {
        switchPriority.text = "Priority: " + currentTower.targetPriority;
    }
}
