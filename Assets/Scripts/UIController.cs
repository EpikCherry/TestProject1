using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Pause()
    {
        animator.Play("FadeIn");
        Time.timeScale  = 0;
    }
    public void Resume()
    {
        animator.Play("FadeOut");
        Time.timeScale = 1;
    }

    public void PlayEndAnimation()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1f);
        animator.Play("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}