using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScene : MonoBehaviour
{
    public void ResetButton()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
       // GameManager.Instance.ResetGame();
        StartCoroutine(UIManager.Instance.LoadScene("GameScene", GameManager.GameState.Game));
    }

    public void ResumeScene()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        GameManager.Instance.state = GameManager.GameState.Game;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
       // GameManager.Instance.ResetGame();
        StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));
    }
}
