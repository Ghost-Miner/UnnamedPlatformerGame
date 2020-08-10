using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    #region Variables
    //Put variables here
    public Dropdown   resolutionDropdown;
    public GameObject VideoSettings;
    public GameObject AudioSettings;
    public GameObject Controls;

    #endregion

    #region Settings Category Buttons

    public void VideoSettingsButton()
    {
        VideoSettings.SetActive(true);
        AudioSettings.SetActive(false);
        Controls.SetActive(false);
    }
    public void AudioSettingsButton()
    {
        VideoSettings.SetActive(false);
        AudioSettings.SetActive(true);
        Controls.SetActive(false);
    }
    public void ControlsButton()
    {
        VideoSettings.SetActive(false);
        AudioSettings.SetActive(false);
        Controls.SetActive(true);
    }

    #endregion

    #region Video Settings
    //Write video settings stuff here

    public void SetQuality(int qualityIndex) // Quality changing dropdown
    {
        Debug.Log("Quality:" + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen) // Fullscreen toggle
    {
        Debug.Log("Fullscreen:" + isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    // Write Resolution settings here \/


    #endregion

    #region AudioSettings

    //Write Audio settings stuff here

    #endregion

    #region Controls

    //Write controls settings stuff here
    //You can make extra script if there's too much stuff

    #endregion

    #region Other

    //If you want to add something else which isn't audio, video or controls related, write it here.

    #endregion

    #region Back to menu
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}