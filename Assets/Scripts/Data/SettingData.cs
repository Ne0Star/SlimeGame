using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;

//
// Отвечает за хранение данных настроек
//
[Serializable]
public class SettingData
{
    public bool SFX = true;
    public bool MUSIK = true;
    public int FPSIndex = 0;
    public int FPS = 30;

    public GraphicsPreset graphicsPreset = 0;
    public PhysicsPreset physicsPreset = 0;

    public enum GraphicsPreset
    {
        Low,
        Medium
    }
    public enum PhysicsPreset
    {
        Low,
        Medium
    }

}
