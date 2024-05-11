using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public float fadeDuration = 1.0f;

    private AudioSource audioSource;
    private bool isFading;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFading)
                StartCoroutine(TransitionAudio(audioClips[1]));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFading)
                StartCoroutine(TransitionAudio(audioClips[0]));
        }
    }

    private IEnumerator TransitionAudio(AudioClip nextClip)
    {
        isFading = true;

        float timer = 0;
        float startVolume = audioSource.volume;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
            yield return null;
        }

        audioSource.clip = nextClip;
        audioSource.Play();

        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, startVolume, timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
        isFading = false;
    }
}
