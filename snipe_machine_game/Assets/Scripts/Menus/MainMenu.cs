using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle postProcessingToggle;
    [SerializeField] GameObject ppObj;

    void Start() {
        fullscreenToggle.isOn = Screen.fullScreen;
        if (PlayerPrefs.HasKey("postProcessing")) {
            int value = PlayerPrefs.GetInt("postProcessing");
            if (value==0) postProcessingToggle.isOn = false;
            else postProcessingToggle.isOn = true;
        } else {
            TogglePostProcessing();
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    public void CloseOptions() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ToggleFullscreen(bool on) {
        Screen.fullScreen = on;
    }

    public void TogglePostProcessing() {
        int value;
        bool on = postProcessingToggle.isOn;
        if (on) value = 1;
        else value = 0;
        PlayerPrefs.SetInt("postProcessing",value);
        ppObj.SetActive(on);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
