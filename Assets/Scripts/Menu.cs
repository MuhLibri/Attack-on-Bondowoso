using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject loadSaveMenu;
    public GameObject mainMenu;
    public GameObject statisticsMenu;
    public GameObject settingsMenu;
    public Slider volumeSlider;

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowSaveStates()
    {
        loadSaveMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowStatistics()
    {
        statisticsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        loadSaveMenu.SetActive(false);
        statisticsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetGameSessionName(string sessionName)
    {
        PlayerPrefs.SetString("GameSessionName", sessionName);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
