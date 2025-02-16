using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField] private Toggle windowedToggle;         // Toggle for enabling windowed mode.
    [SerializeField] private TMP_Dropdown resolutionDropdown; // Dropdown for selecting screen resolution.
    [SerializeField] private TMP_Dropdown qualityDropdown;  // Dropdown for selecting graphics quality.

    private Resolution[] availableResolutions;       // Array to store available screen resolutions.

    // Initializes the settings menu and loads previously saved settings.
    void Start()
    {
        // Populate the resolution dropdown with available screen resolutions.
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            options.Add(option);

            if (availableResolutions[i].width == Screen.currentResolution.width
                && availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        LoadSettings(); // Load saved settings.

        // Add listeners for settings changes.
        resolutionDropdown.onValueChanged.AddListener(ApplyResolution);
        windowedToggle.onValueChanged.AddListener(ApplyWindowed);
        qualityDropdown.onValueChanged.AddListener(ApplyQuality);
    }

    // Applies windowed or fullscreen mode based on toggle state.
    public void ApplyWindowed(bool isWindowed)
    {
        Screen.fullScreen = !isWindowed;
        PlayerPrefs.SetInt("Windowed", isWindowed ? 1 : 0);
        PlayerPrefs.Save();

        if(isWindowed == true)
        {
            Debug.Log("Windowed");
        }
        else
        {
            Debug.Log("Fullscreen");
        }
    }

    // Applies the selected screen resolution.
    public void ApplyResolution(int index)
    {
        Resolution res = availableResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();

        Debug.Log(res);
    }

    // Applies the selected graphics quality.
    public void ApplyQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        PlayerPrefs.Save();

        Debug.Log(qualityIndex);
    }

    // Loads previously saved settings for video and audio.
    private void LoadSettings()
    {
        bool isWindowed = PlayerPrefs.GetInt("Windowed", Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = !isWindowed;
        windowedToggle.isOn = isWindowed;

        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", availableResolutions.Length - 1);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        ApplyResolution(resolutionIndex);

        int qualityIndex = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();
        ApplyQuality(qualityIndex);
    }
}