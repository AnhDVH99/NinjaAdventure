using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class InputManager : Singleton<InputManager>
{
    public TMP_InputField playerNameInputField;
    public string playerName;
    private const string PlayerNameKey = "PlayerName";
    public void SetPlayerName()
    {
        playerName = playerNameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }
        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync("PlayScene");
    }

    public string GetSavedPlayerName()
    {
        return PlayerPrefs.GetString(PlayerNameKey, "Player");
    }
    
}