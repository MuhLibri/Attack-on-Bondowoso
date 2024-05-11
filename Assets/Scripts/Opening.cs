using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Opening : MonoBehaviour
{
    private GameObject canvas;
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        StartCoroutine(OpeningCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Main");
    }

    private IEnumerator OpeningCutscene()
    {
        yield return new WaitForSeconds(5);
        canvas.SetActive(false);
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Main");
    }
}
