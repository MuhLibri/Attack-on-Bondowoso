using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]
    private static int questIndex = 0;
    public static Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        currentQuest = quests[questIndex];
        currentQuest.StartQuest();

        questTitleText = questTitle.GetComponent<TextMeshProUGUI>();
        questObjectiveText = questObjective.GetComponent<TextMeshProUGUI>();
        questProgressText = questProgress.GetComponent<TextMeshProUGUI>();

        questTitleText.text = currentQuest.questTitle;
        questObjectiveText.text = currentQuest.questObjective;
        questProgressText.text = currentQuest.GetTargetKill() != 0? (currentQuest.GetKilled().ToString() + "/" + currentQuest.GetTargetKill().ToString()) : ("");
    }

    // Update is called once per frame
    void Update()
    {
        questProgressText.text = currentQuest.GetTargetKill() != 0? (currentQuest.GetKilled().ToString() + "/" + currentQuest.GetTargetKill().ToString()) : ("");

        if (questIndex < quests.Length && currentQuest.IsFinished()) {
            currentQuest.FinishQuest();
            questIndex++;

            currentQuest = quests[questIndex];
            currentQuest.StartQuest();
            questTitleText.text = currentQuest.questTitle;
            questObjectiveText.text = currentQuest.questObjective;
        }
    }

    public static void AddKilled() {
        currentQuest.AddKilled();
    }

    public static int GetQuestIndex() {
        return questIndex;
    }

    public void SetCurrentQuest(int loadedQuestIndex) {
        currentQuest.ResetQuest();
        questIndex = loadedQuestIndex;
        currentQuest = quests[questIndex];
        currentQuest.ResetQuest();
        currentQuest.StartQuest();

        questTitleText.text = currentQuest.questTitle;
        questObjectiveText.text = currentQuest.questObjective;
        questProgressText.text = currentQuest.GetTargetKill() != 0? (currentQuest.GetKilled().ToString() + "/" + currentQuest.GetTargetKill().ToString()) : ("");
    }

    public static void SkipCurrentQuest() {
        currentQuest.SkipQuest();
    }
}
