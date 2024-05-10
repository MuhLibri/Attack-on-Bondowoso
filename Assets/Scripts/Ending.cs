using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        StartCoroutine(EndingCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Menu");
    }

    private IEnumerator EndingCutscene()
    {
        yield return new WaitForSeconds(5);
        canvas.SetActive(false);
    }
}
