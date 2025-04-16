using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScene : MonoBehaviour
{
    public void NextButton()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        currentLevel++;

        if (currentLevel > 3)
        {
            StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));
            return;
        }
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
        GameManager.Instance.UpdateNextLevel();
        string nameLevel = "Level " + currentLevel;
        GameManager.Instance.numberItem = 0;
        StartCoroutine(UIManager.Instance.LoadScene(nameLevel, GameManager.GameState.Game));
    }


    public void RetryButton()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        string nameLevel = "Level " + PlayerPrefs.GetInt("CurrentLevel", 1);
        GameManager.Instance.numberItem = 0;
        StartCoroutine(UIManager.Instance.LoadScene(nameLevel, GameManager.GameState.Game));
    }

    public void HomeScene()
    {
        Time.timeScale = 1f;
        GameManager.Instance.numberItem = 0;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));
    }
}
