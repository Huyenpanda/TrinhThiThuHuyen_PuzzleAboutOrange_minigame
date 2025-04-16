using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI LevelText;

    private void Update()
    {
       // this.LevelText.text = "Level " + PlayerPrefs.GetInt("CurrentLevel", 0).ToString();
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        GameManager.Instance.state = GameManager.GameState.Pause;
    }

    public void RetryButton()
    {
        Debug.Log("RetryButton");
        Time.timeScale = 1f;
        //   GameManager.Instance.numberItem = 0;
         AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
         string nameLevel = "Level " + PlayerPrefs.GetInt("CurrentLevel", 1);
          StartCoroutine(UIManager.Instance.LoadScene(nameLevel, GameManager.GameState.Game));
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        //GameManager.Instance.ResetGame();
        //StartCoroutine(UIManager.Instance.LoadScene("Level 1", GameManager.GameState.Game));
    }
    public void HomeScene()
    {
        Debug.Log("HomeScene");
        Time.timeScale = 1f;
        GameManager.Instance.numberItem = 0;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
       // StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));
        StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));

    }
}
