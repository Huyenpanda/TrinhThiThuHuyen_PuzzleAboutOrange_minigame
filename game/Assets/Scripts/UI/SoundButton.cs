using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOffIcon;
    [SerializeField] private Sprite soundOnIcon;

    private void Start()
    {
        this.UpdateButtonIcon();
    }

    public void ToggleSound()
    {
        AudioManager.Instance.sfxSource.mute = !AudioManager.Instance.sfxSource.mute;
        AudioManager.Instance.musicSource.mute = !AudioManager.Instance.musicSource.mute;

        PlayerPrefs.SetInt("Muted", AudioManager.Instance.sfxSource.mute ? 1 : 0);
        PlayerPrefs.SetInt("Muted", AudioManager.Instance.musicSource.mute ? 1 : 0);
        PlayerPrefs.Save();
        UpdateButtonIcon();
    }

    void UpdateButtonIcon()
    {
        this.GetComponent<Button>().image.sprite = AudioManager.Instance.sfxSource.mute ? soundOffIcon : soundOnIcon;
    }
}
