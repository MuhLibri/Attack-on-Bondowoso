using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject LoadSaveMenu;
    public GameObject MainMenu;
    public GameObject StatisticsMenu;
    public GameObject SettingsMenu;


    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowSaveStates()
    {
        LoadSaveMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ShowStatistics()
    {
        StatisticsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ShowSettings()
    {
        SettingsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void HideMenu()
    {
        MainMenu.SetActive(true);
        LoadSaveMenu.SetActive(false);
        StatisticsMenu.SetActive(false);
        SettingsMenu.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
