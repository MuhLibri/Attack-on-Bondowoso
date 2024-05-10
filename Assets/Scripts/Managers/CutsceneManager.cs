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
                if (instance == null)
                {
                    Debug.LogError("CutsceneManager instance not found in the scene.");
                }
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
        climax.Add(("Roro", "We've reached the heart of the jungle. The treasure must be close!"));
        climax.Add(("Bondowoso", "Yes, let's find it quickly before someone else does!"));
    }

    public void PlayClimax()
    {
        cutsceneCanvas.SetActive(true);
        StartCoroutine(DisplayDialogues(climax, 5.0f));
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
