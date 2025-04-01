using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Button[] volumeButtons;
    [SerializeField] private Sprite filledSprite;
    [SerializeField] private Sprite emptySprite;

    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;

    private int currentVolumeLevel = 7;
    private int savedVolumeLevel = 7;

    private bool isMuted = false;

    private const string VolumePrefKey = "VolumeLevel";
    private const string MutePrefKey = "Muted";

    void Start()
    {
        currentVolumeLevel = PlayerPrefs.GetInt(VolumePrefKey, 7);
        isMuted = PlayerPrefs.GetInt(MutePrefKey, 0) == 1;
        savedVolumeLevel = currentVolumeLevel;

        AudioListener.volume = isMuted ? 0f : currentVolumeLevel / 9f;

        UpdateVolumeUI();
        UpdateMuteIcon();
    }

    public void SetVolume(int level)
    {
        currentVolumeLevel = level;
        isMuted = false;

        AudioListener.volume = level / 9f;
        SaveSettings();

        UpdateVolumeUI();
        UpdateMuteIcon();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            savedVolumeLevel = currentVolumeLevel;
            AudioListener.volume = 0f;
        }
        else
        {
            currentVolumeLevel = savedVolumeLevel;
            AudioListener.volume = currentVolumeLevel / 9f;
        }

        SaveSettings();
        UpdateVolumeUI();
        UpdateMuteIcon();
    }

    void UpdateVolumeUI()
    {
        for(int i = 0; i < volumeButtons.Length; i++)
        {
            Image img = volumeButtons[i].GetComponent<Image>();
            img.sprite = (isMuted || i >= currentVolumeLevel) ? emptySprite : filledSprite;
        }
    }

    void UpdateMuteIcon()
    {
        muteButton.GetComponent<Image>().sprite = isMuted ? soundOffIcon : soundOnIcon;
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt(VolumePrefKey, currentVolumeLevel);
        PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}