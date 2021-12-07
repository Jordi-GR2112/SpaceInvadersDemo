using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
/// 

public class UIObjectFader : MonoBehaviour
{
    public CanvasGroup ObjectToFade;
    public float fadeTime;

    public UnityEvent OnFadeIn;
    public UnityEvent OnFadeOut;

    bool hasFade = true;

    public void FadeOut()
    {
        StartCoroutine(ToFadeOut());
    }

    public void FadeIn()
    {
        StartCoroutine(ToFadeIn());
    }

    IEnumerator ToFadeOut()
    {
        OnFadeOut?.Invoke();
        ObjectToFade.DOFade(0, fadeTime);

        yield return new WaitForSeconds(fadeTime);
        ObjectToFade.blocksRaycasts = false;
        hasFade = true;
    }

    IEnumerator ToFadeIn()
    {
        yield return new WaitUntil(() => hasFade);

        ObjectToFade.DOFade(1, fadeTime);

        yield return new WaitForSeconds(fadeTime);
        ObjectToFade.blocksRaycasts = true;

        OnFadeIn?.Invoke();
        hasFade = false;
    }
}
