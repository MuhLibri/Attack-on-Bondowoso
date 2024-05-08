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

    public AudioSource musicSource;
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

    public void SetVolume(float volume)
    {
        Debug.Log("Volume slider value: " + volume);
        musicSource.volume = volume;
        Debug.Log("Music volume set to: " + musicSource.volume);
    }
}
