using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private GameObject sourceObject;

    public AudioClip activeClip;

    public AudioSource audioCorce;

    //
    // При обьявлении данного менеджера создается объект глобальный - источник отвечающий за звуки
    //

    public AudioManager()
    {
        sourceObject = new GameObject("AudioCorce");
        audioCorce = sourceObject.AddComponent<AudioSource>();
    }

    public void LoadAndPlay(SongType type, SongPlayMode mode, string name)
    {
        var clip = (AudioClip)Resources.Load("song/" + type.ToString() + "/" + name);
        if (mode == SongPlayMode.ADD)
        {
            audioCorce.PlayOneShot(clip);
        }
        else
        {
            audioCorce.Stop();
            audioCorce.PlayOneShot(clip);
        }
    }

    public enum SongType
    {
        SFX,
        MUSIK
    }

    public enum SongPlayMode
    {
        ADD,
        STOPALL
    }

}
