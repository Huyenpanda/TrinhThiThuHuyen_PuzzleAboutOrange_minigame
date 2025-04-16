using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LossScene : MonoBehaviour
{
    private bool canClickToReset = false;

    private void OnEnable()
    {
       
        StartCoroutine(WaitBeforeAllowClick());
    }

    private IEnumerator WaitBeforeAllowClick()
    {
        yield return new WaitForSeconds(1f);
        canClickToReset = true;
    }

    private void Update()
    {
        if (canClickToReset && Input.GetMouseButtonDown(0))
        {
            ResetButton();
        }
    }

    public void ResetButton()
    {
        CountdownTimer timer = FindObjectOfType<CountdownTimer>();
        timer.ResetTimer();
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
        //GameManager.Instance.ResetGame();
        string nameLevel = "Level " + PlayerPrefs.GetInt("CurrentLevel", 1);
        StartCoroutine(UIManager.Instance.LoadScene(nameLevel, GameManager.GameState.Game));
    }

    public void HomeScene()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
       // GameManager.Instance.ResetGame();
        StartCoroutine(UIManager.Instance.LoadScene("MenuScene", GameManager.GameState.Menu));
    }
}
