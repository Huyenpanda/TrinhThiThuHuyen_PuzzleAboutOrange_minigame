
using UnityEngine;
using Hyb.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManualSingletonMono<GameManager>
{
    public enum GameState { Menu, Game, Pass, Loss, Pause, Loading }
    public GameState state;

    public int numberItem = 0;
    
    private void Update()
    {
       // PlayerPrefs.DeleteAll();
        this.OnStateChanged();
    }

    private void OnStateChanged()
    {
        switch (state)
        {
            case GameState.Menu:
                UIManager.Instance.MenuScene();
                break;
            case GameState.Game:
                UIManager.Instance.GameScene();
                break;
            case GameState.Pass:
                UIManager.Instance.PassScene();
                break;
            case GameState.Loss:
                UIManager.Instance.LossScene();
                break;
            case GameState.Pause:
                UIManager.Instance.PauseScene();
                break;
            case GameState.Loading:
                break;
        }
    }

    public void UpdateNextLevel()
    {
        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        if (currentLevel > unlockLevel)
        {
            unlockLevel = currentLevel;
        }
        PlayerPrefs.SetInt("UnlockedLevel", unlockLevel);
        PlayerPrefs.Save();
    }
}
