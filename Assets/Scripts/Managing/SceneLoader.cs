using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader
{
    public GameObject loadPanel, loadImage;
    public int BackSceneIndex = 0;

    private Image m_loadImage;
    public SceneLoader()
    {
        loadPanel = GameObject.FindGameObjectWithTag("LoadPanel");
        loadImage = GameObject.FindGameObjectWithTag("LoadPanelImage");
        m_loadImage = loadImage.GetComponent<Image>();
        loadPanel.GetComponent<Animator>().Play("closeLoadPanel");
    }

    public enum LoadMode
    {
        AsyncAddtive,
        AsyncSingle,
        Addtive,
        Single
    }

    public IEnumerator OpenSceneAsyncSingle(int index, Slider slider, Text text)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            text.text = progress.ToString();
            slider.value = progress;
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncAddtive(int index, Slider slider, Text text)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            text.text = progress.ToString();
            slider.value = progress;
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncSingle(int index, Text text)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            text.text = progress.ToString();
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncAddtive(int index, Text text)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            text.text = progress.ToString();
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncSingle(int index, Slider slider)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncAddtive(int index, Slider slider)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    public IEnumerator OpenSceneAsyncSingle(int index, string animationName)
    {
        loadPanel.GetComponent<Animator>().Play(animationName);

        yield return new WaitForSeconds(0.5f);
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!time.isDone)
        {
            loadImage.transform.parent.gameObject.SetActive(true);
            float progress = Mathf.Clamp01(time.progress / 0.999f);
            Debug.Log(progress);
            m_loadImage.fillAmount = progress;
            yield return null;
        }
    }
    public IEnumerator OpenSceneAsyncAddtive(int index)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        while (!time.isDone)
        {
            float progress = Mathf.Clamp01(time.progress / .9f);
            yield return null;
        }
    }
    public void OpenSceneSingle(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    public void OpenSceneAddtive(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }
}
