using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questTitle;
    public GameObject questObjective;
    public GameObject questProgress;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questObjectiveText;
    public TextMeshProUGUI questProgressText;


    public Quest[] quests;
    public int questIndex = 0;
    public static Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        currentQuest = quests[questIndex];
        questTitleText = questTitle.GetComponent<TextMeshProUGUI>();
        questObjectiveText = questObjective.GetComponent<TextMeshProUGUI>();
        questProgressText = questProgress.GetComponent<TextMeshProUGUI>();

        questTitleText.text = currentQuest.questTitle;
        questObjectiveText.text = currentQuest.questObjective;
        questProgressText.text = currentQuest.GetKilled().ToString() + "/" + currentQuest.GetTargetKill().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        questProgressText.text = currentQuest.GetKilled().ToString() + "/" + currentQuest.GetTargetKill().ToString();
    }

    public static void AddKilled() {
        currentQuest.AddKilled();
    }
}
