using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{

    private string SettingFile = "Setting";

    private SettingData DefaultSettingData = new SettingData()
    {
        SFX = true,
        MUSIK = true,
        graphicsPreset = (int)SettingData.GraphicsPreset.Low,
        physicsPreset = (int)SettingData.PhysicsPreset.Low
    };

    public SettingData EndSavedData = null;

    public SaveManager()
    {
        EndSavedData = LoadSetting();
        SaveSetting(EndSavedData);
    }

    public void SaveSetting(SettingData newData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SettingFile + ".dat");
        SettingData data = null;
        data = newData;
        bf.Serialize(file, data);
        file.Close();
        EndSavedData = data;
        Application.targetFrameRate = data.FPS;
    }

    public SettingData LoadSetting()
    {
        SettingData loaded_data = null;
        if (File.Exists(Application.persistentDataPath + "/" + SettingFile + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SettingFile + ".dat", FileMode.Open);
            SettingData data = (SettingData)bf.Deserialize(file);
            file.Close();

            loaded_data = data;
            Debug.Log("Настройки получены" + Application.persistentDataPath + "/" + SettingFile + ".dat");

        }
        else
        {
            Debug.Log("Не удалось получить настройки, создаём новые...");
            SaveSetting(DefaultSettingData);
            Debug.Log("Настройки по умолчанию выставлены !");
            return DefaultSettingData;
        }
        return loaded_data;
    }
}

