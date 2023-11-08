using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int currentCurrency;
    public int maxCurrency;

    public int towerCurrencyCreationAmount;

    public bool SpawnTower()
    {
        if (currentCurrency > towerCurrencyCreationAmount)
        {
            currentCurrency -= towerCurrencyCreationAmount;
            return true;
        }
        else
        {
            return false;
        }

        return false;
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
