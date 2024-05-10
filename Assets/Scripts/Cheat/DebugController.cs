using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input = "";
    public GameObject player;
    public GameObject orbDamage;
    public GameObject orbHealth;
    public GameObject orbSpeed;
    public static DebugCommand NO_DAMAGE;
    public static DebugCommand ONE_HIT_KILL;
    public static DebugCommand MOTHERLODE;
    public static DebugCommand TWICE_SPEED;
    public static DebugCommand FULL_HP_PET;
    public static DebugCommand KILL_PET;
    public static DebugCommand ORB_DAMAGE;
    public static DebugCommand ORB_HEALTH;
    public static DebugCommand ORB_SPEED;
    public static DebugCommand SKIP;
    public List<DebugCommand> commandList;

    void Awake() {
        NO_DAMAGE = new("no_damage", "make the player can't die", "no_damage", () => {
            if (PlayerHealth.IsNoDamage()) {
                PlayerHealth.DeactivateNoDamage();
                Debug.Log("No Damage deactivated");
            }
            else {
                PlayerHealth.ActivateNoDamage();
                Debug.Log("No Damage activated");
            }
        });

        ONE_HIT_KILL = new("one_hit_kill", "make the enemy died in one hit", "one_hit_kill", () => {
            if (EnemyHealth.IsOneHitKil()) {
                EnemyHealth.DeactivateOneHitKill();
                Debug.Log("One Hit Kill deactivated");
            }
            else {
                EnemyHealth.ActivateOneHitKill();
                Debug.Log("One Hit Kill activated");
            }
        });

        MOTHERLODE = new("motherlode", "Make the player gold unlimited", "motherlode", () => {
            if (PlayerGold.IsMotherlode()) {
                PlayerGold.DeactivateMotherlode();
                Debug.Log("Motherlode deactivated");
            }
            else {
                PlayerGold.ActivateMotherlode();
                Debug.Log("Motherlode activated");
            }
        });

        TWICE_SPEED = new("twice_speed", "Make the player movement 2 times faster", "twice_speed", () => {
            if (PlayerMovement.IsTwiceSpeed()) {
                PlayerMovement.DeactivateTwiceSpeed();
                Debug.Log("Twice Speed deactivated");
            }
            else {
                PlayerMovement.ActivateTwiceSpeed();
                Debug.Log("Twice Speed activated");
            }
        });

        FULL_HP_PET = new("full_hp_pet", "Make the pet unkillable", "full_hp_pet", () => {
            // TO DO Implement
            if (PlayerHealth.IsNoDamage()) {
                PlayerHealth.DeactivateNoDamage();
                Debug.Log("Full Hp Pet deactivated");
            }
            else {
                PlayerHealth.ActivateNoDamage();
                Debug.Log("Full Hp Pet activated");
            }            
        });

        KILL_PET = new("kill_pet", "Instantly kill all pet", "kill_pet", () => {
            // TO DO Implement
            Debug.Log("Kill Pet activated");
        });

        ORB_DAMAGE = new("orb_damage", "Instantly spawn an increase damage orb", "orb_damage", () => {
            Transform playerTransform = player.GetComponent<Transform>();
            Vector3 orbPosition = playerTransform.position;
            Quaternion orbRotation = playerTransform.rotation;
            orbPosition.x += 3;
            Instantiate(orbDamage, orbPosition, orbRotation);
            Debug.Log("Orb Damage activated");
        });

        ORB_HEALTH = new("orb_health", "Instantly spawn a restore health orb", "orb_health", () => {
            Transform playerTransform = player.GetComponent<Transform>();
            Vector3 orbPosition = playerTransform.position;
            Quaternion orbRotation = playerTransform.rotation;
            orbPosition.x += 3;
            Instantiate(orbHealth, orbPosition, orbRotation);
            Debug.Log("Orb Health activated");
        });
        
        ORB_SPEED = new("orb_speed", "Instantly spawn an increase speed orb", "orb_speed", () => {
            Transform playerTransform = player.GetComponent<Transform>();
            Vector3 orbPosition = playerTransform.position;
            Quaternion orbRotation = playerTransform.rotation;
            orbPosition.x += 3;
            Instantiate(orbSpeed, orbPosition, orbRotation);
            Debug.Log("Orb Speed activated");
        });
        
        SKIP = new("skip", "Skip current quest", "skip", () => {
            QuestManager.SkipCurrentQuest();
            Debug.Log("Skip quest activated");
        });

        commandList = new List<DebugCommand>{
            NO_DAMAGE,
            ONE_HIT_KILL,
            MOTHERLODE,
            TWICE_SPEED,
            FULL_HP_PET,
            KILL_PET,
            ORB_DAMAGE,
            ORB_HEALTH,
            ORB_SPEED,
            SKIP
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
