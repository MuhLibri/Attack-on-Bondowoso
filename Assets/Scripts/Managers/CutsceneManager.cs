using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public GameObject cutsceneCanvas;
    public TextMeshProUGUI cutsceneName;
    public TextMeshProUGUI cutsceneDialogue;
    public List<(string name, string text)> climax;

    private static CutsceneManager instance;
    public static CutsceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CutsceneManager>();
            }
            return instance;
        }
    }

    public void Start()
    {
        climax = new List<(string, string)>();
        AddClimax();
    }

    public void AddClimax()
    {
        climax.Add(("Roro", "Bondowoso, I cannot let you fulfill your promise. I will stop you before you build a thousand temples!"));
        climax.Add(("Bondowoso", "Roro Jonggrang, why do you resist our union? I have sworn to build the temples as you requested. Why this sudden change of heart?"));
        climax.Add(("Roro", "I have seen the suffering of my people. I cannot let you continue your reign of terror. I will not be your puppet queen!"));
        climax.Add(("Bondowoso", "You will regret this, Roro Jonggrang. I will build the temples with or without your consent. You will be the last statue in my collection!"));
        climax.Add(("Roro", "I will never be your statue, Bondowoso. I will fight you with all my might!"));
        climax.Add(("Bondowoso", "Then let the battle begin!"));
    }

    public void PlayClimax()
    {
        cutsceneCanvas.SetActive(true);
        StartCoroutine(DisplayDialogues(climax, 3.0f));
    }

    public IEnumerator DisplayDialogues(List<(string name, string text)> dialogues, float autoProceedDelay)
    {
        foreach (var dialogue in dialogues)
        {
            cutsceneName.text = dialogue.name;
            cutsceneDialogue.text = dialogue.text;
            yield return new WaitForSeconds(autoProceedDelay);
        }
        cutsceneCanvas.SetActive(false);
    }
}
