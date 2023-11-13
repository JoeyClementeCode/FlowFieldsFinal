using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int currentCurrency = 10;
    public int maxCurrency = 100;

    public int towerCurrencyCreationAmount = 5;

    public bool SpawnTower()
    {
        if (currentCurrency >= towerCurrencyCreationAmount)
        {
            currentCurrency -= towerCurrencyCreationAmount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GainCurrency(int amount)
    {
        currentCurrency += amount;

        if (currentCurrency > maxCurrency)
        {
            currentCurrency = maxCurrency;
        }
    }
        
}
