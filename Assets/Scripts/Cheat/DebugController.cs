using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input = "";
    public static DebugCommand NO_DAMAGE;
    public List<DebugCommand> commandList;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake() {
        NO_DAMAGE = new("no_damage", "make the player can't die", "no_damage", () => {
            Debug.Log("Ngga bisa mati cuy");
            if (PlayerHealth.IsNoDamage()) {
                PlayerHealth.DeactivateNoDamage();
                Debug.Log("No Damage deactivated");
            }
            else {
                PlayerHealth.ActivateNoDamage();
                Debug.Log("No Damage activated");
            }
        });

        commandList = new List<DebugCommand>{
            NO_DAMAGE,
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            showConsole = !showConsole;
            input = showConsole? "" : input;
        }

        // Handdle keyboard input
        HanddleInput();
    }

    private void OnGUI()
    {
        if (showConsole)
        {
            float y = Screen.height - 30;
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }
    }

    void HanddleInput() {
        if (showConsole)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b')
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    // TO DO Implement cheat
                    Debug.Log("Input: " + input);
                    foreach (DebugCommand debugCommand in commandList) {
                        if (input == debugCommand.GetCommandName()) {
                            debugCommand.Invoke();
                        }
                    }

                    input = "";
                }
                else
                {
                    input += (c == '`' ? "" : c);
                }
            }
        }
    }
}
