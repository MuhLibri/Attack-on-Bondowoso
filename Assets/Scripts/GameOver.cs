using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static string mainScene = "Main";
    public GameObject gameOverPanel;
    public TextMeshProUGUI sessionAccuracy;
    public TextMeshProUGUI sessionDistance;
    public TextMeshProUGUI sessionPlaytime;
    public TextMeshProUGUI sessionGold;
    public TextMeshProUGUI sessionKill;
    public TextMeshProUGUI sessionSave;
    public TextMeshProUGUI countdownText;
    public const float countdownDuration = 15f;

    private static GameOver instance;
    public static GameOver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameOver>();
                if (instance == null)
                {
                    Debug.LogError("GameOver instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    public void ShowGameOver()
    {
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
        sessionAccuracy.text = "Accuracy: " + accuracy.ToString("F1") + "%";
        sessionDistance.text = "Distance: " + distance.ToString("F1") + " kilometer";
        sessionPlaytime.text = "Playtime: " + FormatTime(playtime);
        sessionGold.text = "Gold: " + gold.ToString();
        sessionKill.text = "Kill: " + kill.ToString();
        sessionSave.text = "Save: " + save.ToString();
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        float timer = countdownDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }
        ShowMenu();
    }

    public void RestartGame()
    {
        StatisticsManager.Instance.SaveStatistics();
        QuestManager.ResetQuestManager();
        SceneManager.LoadScene(mainScene);
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
