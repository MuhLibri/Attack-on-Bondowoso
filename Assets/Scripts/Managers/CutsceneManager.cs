using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public List<(string name, string text)> opening;
    public List<(string name, string text)> climax;
    public List<(string name, string text)> ending;

    public GameObject cutsceneCanvas;
    public TextMeshProUGUI cutsceneName;
    public TextMeshProUGUI cutsceneDialogue;

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
                    GameObject singletonObject = new GameObject("CutsceneManager");
                    instance = singletonObject.AddComponent<CutsceneManager>();
                }
            }
            return instance;
        }
    }

    public void Start()
    {
        opening = new List<(string, string)>();
        climax = new List<(string, string)>();
        ending = new List<(string, string)>();

        AddOpening();
        AddClimax();
        AddEnding();
    }

    public void AddOpening()
    {
        opening.Add(("Roro", "Welcome, traveler! Are you ready for an adventure?"));
        opening.Add(("Bondowoso", "Indeed, I am. Lead the way!"));
    }

    public void AddClimax()
    {
        climax.Add(("Roro", "We've reached the heart of the jungle. The treasure must be close!"));
        climax.Add(("Bondowoso", "Yes, let's find it quickly before someone else does!"));
    }

    public void AddEnding()
    {
        ending.Add(("Roro", "We did it! The treasure is ours!"));
        ending.Add(("Bondowoso", "What a journey it has been. Thank you for your help!"));
    }

    public void PlayOpening()
    {
        cutsceneCanvas.SetActive(true);
        StartCoroutine(DisplayDialogues(opening));
    }

    public void PlayClimax()
    {
        cutsceneCanvas.SetActive(true);
        StartCoroutine(DisplayDialogues(climax));
    }

    public void PlayEnding()
    {
        cutsceneCanvas.SetActive(true);
        StartCoroutine(DisplayDialogues(ending));
    }

    private IEnumerator DisplayDialogues(List<(string name, string text)> dialogues)
    {
        bool spaceReleased = true;
        foreach (var dialogue in dialogues)
        {
            cutsceneName.text = dialogue.name;
            cutsceneDialogue.text = dialogue.text;
            yield return new WaitUntil(() => spaceReleased && Input.GetKeyDown(KeyCode.Space));
            spaceReleased = false;
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
            spaceReleased = true;
        }
        cutsceneCanvas.SetActive(false);
    }
}
