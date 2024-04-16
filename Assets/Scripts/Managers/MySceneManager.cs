using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class MySceneManager : Singleton<MySceneManager>
{
    [SerializeField] CanvasGroup fadeImg;
    float m_fadeSpeed;
    string m_sceneName;

    public bool IsComplete() { return !fadeImg.blocksRaycasts; }




    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnComplete;
    }

    public void ChangeScene(string sceneName, float fadeSpeed)
    {
        m_sceneName = sceneName;
        m_fadeSpeed = fadeSpeed;
        OnStart();
    }

    void OnStart()
    {
        StartCoroutine(CorFadeOut());
        fadeImg.blocksRaycasts = true;
    }

    void OnComplete(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CorFadeIn());
    }

    IEnumerator CorFadeIn()
    {
        while (fadeImg.alpha > 0)
        {
            fadeImg.alpha -= Time.deltaTime / m_fadeSpeed;
            yield return null;
        }
        fadeImg.blocksRaycasts = false;
        StopCoroutine(CorFadeIn());
    }

    IEnumerator CorFadeOut()
    {
        while (fadeImg.alpha < 1)
        {
            fadeImg.alpha += Time.deltaTime / m_fadeSpeed;
            yield return null;
        }

        SceneManager.LoadScene(m_sceneName);
        StopCoroutine(CorFadeOut());

    }
}
