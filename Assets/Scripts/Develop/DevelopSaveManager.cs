using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DevelopSaveManager
{
    public SlimeMaterial[] UploadMaterials;

    public DevelopSaveManager()
    {
        UploadData();
    }
    public Sprite GetSpriteByName(string val)
    {
        Sprite sprite = null;
        sprite = (Sprite)Resources.Load<Sprite>("textures/material/" + val);
        return sprite;
    }
    public void UploadData()
    {
        UploadMaterials = Resources.LoadAll<SlimeMaterial>("data/materials");

        //string materialJson = File.ReadAllText(Application.persistentDataPath+ "/data/" + "Materials" + ".json");
        //UploadMaterials = JsonUtility.FromJson<SlimeMaterial.Materials>(materialJson);
        //string effectJson = File.ReadAllText(Application.persistentDataPath + "/data/" + "Effects" + ".json");
        //UploadEffects = JsonUtility.FromJson<SlimeEffect.Effects>(effectJson);
    }
    public void SaveData()
    {

    }
}
