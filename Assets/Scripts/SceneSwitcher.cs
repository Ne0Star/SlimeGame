using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public int LoadSceneIndex;
    public string animationName;

    private Button btn;
    private SceneLoader loader;
    void Start()
    {
        loader = GameManager.Instance?.sceneLoader;
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => StartCoroutine(LoadScene()));

    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.2f);
        loader.BackSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loader.OpenSceneAsyncSingle(LoadSceneIndex, animationName));
    }
}
