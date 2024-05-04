using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InventoryUI InventoryUI;
    [SerializeField] private GameObject settingPanel;
    private bool isPause;
    public Player Player => player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ResetPlayer();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            uiManager.OpenClosePanel();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.OpenCloseInventory();
        }
    }

    private void TogglePause()
    {
        isPause = !isPause;
        settingPanel.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }
}