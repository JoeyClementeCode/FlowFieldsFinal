using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public EconomyManager economy;
    [HideInInspector] public UIManager ui;
    
    public bool gamePaused = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitVariables();
        ResumeGame();
        gamePaused = false;
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
        instance = null;
    }
    
    private void InitVariables()
    {
        economy = GetComponent<EconomyManager>();
        ui = GetComponent<UIManager>();
    }
    
    private void PauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0.001f;
            gamePaused = true;
        }
    }

    private void ResumeGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }
}

