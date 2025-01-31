using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static string mainScene = "Main";
    public GameObject gameOverPanel;
    public GameObject questBox;
    public GameObject healthBar;
    public TextMeshProUGUI sessionAccuracy;
    public TextMeshProUGUI sessionDistance;
    public TextMeshProUGUI sessionPlaytime;
    public TextMeshProUGUI sessionGold;
    public TextMeshProUGUI sessionKill;
    public TextMeshProUGUI sessionSave;
    public TextMeshProUGUI countdownText;
    public const float countdownDuration = 15f;

    private AudioSource audioSource;

    private static GameOver instance;
    public static GameOver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameOver>();
            }
            return instance;
        }
    }

    public void ShowGameOver(bool isPlayerDead)
    {
        questBox.SetActive(false);
        healthBar.SetActive(false);
        gameOverPanel.SetActive(true);
        Cursor.lockState = gameOverPanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        StatisticsData statisticsData = StatisticsManager.Instance.GetStatistics();

        int shotsFired = statisticsData.shotsFired;
        int shotsHit = statisticsData.shotsHit;
        float distance = statisticsData.distance;
        float playtime = statisticsData.playtime;
        int gold = statisticsData.gold;
        int kill = statisticsData.kill;
        int save = statisticsData.save;

        float accuracy = (shotsFired > 0) ? Mathf.Min(((float)shotsHit / shotsFired) * 100, 100) : 0f;
        sessionAccuracy.text = "Accuracy: " + accuracy.ToString("F2") + "%";
        sessionDistance.text = "Distance: " + distance.ToString("F3") + " km";
        sessionPlaytime.text = "Playtime: " + FormatTime(playtime);
        sessionGold.text = "Gold Earned: " + gold.ToString();
        sessionKill.text = "Kill Count: " + kill.ToString();
        sessionSave.text = "Save Count: " + save.ToString();

        GameObject continueButton = gameOverPanel.transform.Find("ContinueButton").gameObject;
        GameObject menuButton = gameOverPanel.transform.Find("MenuButton").gameObject;

        if (isPlayerDead)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            continueButton.SetActive(false);
            menuButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(true);
            menuButton.SetActive(false);
        }

        StartCoroutine(StartCountdown(isPlayerDead));
    }

    public IEnumerator StartCountdown(bool isPlayerDead)
    {
        float timer = countdownDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }
        if (isPlayerDead)
        {
            ShowMenu();
        }
        else
        {
            ShowEnding();
        }
    }

    public void RestartGame()
    {
        StatisticsManager.Instance.SaveStatistics();
        QuestManager.ResetQuestManager();
        SceneManager.LoadScene(mainScene);
    }

    public void ShowEnding()
    {
        StatisticsManager.Instance.SaveStatistics();
        SceneManager.LoadScene("Ending");
    }

    public void ShowMenu()
    {
        StatisticsManager.Instance.SaveStatistics();
        SceneManager.LoadScene("Menu");
    }

    public string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
