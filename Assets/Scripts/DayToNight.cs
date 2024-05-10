using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayToNight : MonoBehaviour
{
    public Material daySkybox;
    public Material nightSkybox;
    public float transitionTime = 10f;

    void Start()
    {
        StartCoroutine(TransitionSkybox());
    }

    IEnumerator TransitionSkybox()
    {
        float t = 0;

        while (t < transitionTime)
        {
            t += Time.deltaTime;
            RenderSettings.skybox.Lerp(nightSkybox, daySkybox, t / transitionTime);
            yield return null;
        }
    }
}