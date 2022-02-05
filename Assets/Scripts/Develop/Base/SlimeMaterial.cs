using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Material")]
public class SlimeMaterial : ScriptableObject
{
    public string MaterialName, MaterialInfo;
    public Sprite MaterialImage;

    public List<CraftInfo> MaterialCrafting = new List<CraftInfo>();
    public List<float> MaterialEffecting;

    [Serializable]
    public class CraftInfo
    {
        public SlimeMaterial material;
        public int materialCount;
    }
}
