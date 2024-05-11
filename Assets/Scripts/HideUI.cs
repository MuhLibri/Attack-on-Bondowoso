using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    public GameObject questBox;
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) {
            questBox.SetActive(!questBox.activeSelf);
            healthBar.SetActive(!healthBar.activeSelf);
        }    
    }
}
