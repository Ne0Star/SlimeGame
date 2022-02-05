using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [Header("Активировать менджер ?")]
    public bool
        m_audioManager,
        m_saveManager,
        m_sceneLoader,
        m_eventManager;

    //
    //
    //

    public AudioManager audioManager = null;
    public SaveManager saveManager = null;
    public SceneLoader sceneLoader = null;
    public EventManager eventManager = null;

    void Start()
    {
        GameManager.Instance = this;
        init();
    }

    private void init()
    {
        if (m_audioManager)
        {
            audioManager = new AudioManager();
        }
        if (m_saveManager)
        {
            saveManager = new SaveManager();
        }
        if (m_sceneLoader)
        {
            sceneLoader = new SceneLoader();
        }
        if (m_eventManager)
        {
            eventManager = new EventManager();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (audioManager != null)
            {
                //audioManager.LoadAndPlay(AudioManager.SongType.SFX, AudioManager.SongPlayMode.STOPALL, audioName);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                StartCoroutine(sceneLoader.OpenSceneAsyncSingle(sceneLoader.BackSceneIndex, "openLoadPanel"));
            }
        }
    }

    public IEnumerator OpenSceneAsyncSingle(int index, GameObject animated)
    {


        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!time.isDone)
        {

            float progress = Mathf.Clamp01(time.progress / .9f);
            yield return null;
        }
    }
}
