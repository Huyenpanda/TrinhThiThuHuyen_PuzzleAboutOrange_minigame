using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    public void PlayButton()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.button);
       // GameManager.Instance.ResetGame();
        StartCoroutine(UIManager.Instance.LoadScene("Level 1", GameManager.GameState.Game));
    }
}
