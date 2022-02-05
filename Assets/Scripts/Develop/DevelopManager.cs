using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DevelopManager : Singleton<DevelopManager>
{
    public DevelopSaveManager develop = null;

    public OnDevelop_SelectItem onSelectItem;

    public void Awake()
    {
        DevelopManager.Instance = (DevelopManager)this;

        develop = new DevelopSaveManager();

    }
    public void Start()
    {
        if (DevelopManager.Instance == null)
            DevelopManager.Instance = (DevelopManager)this;



        //List<SlimeMaterial> ef = new List<SlimeMaterial>();
        //SlimeMaterial effect = new SlimeMaterial()
        //{
        //    MaterialName = "Меня зовут материал",
        //    MaterialInfo = "Я материал",
        //    MaterialUID = 12,
        //    MaterialImage = "k",
        //    MaterialEfficiencyUID = new int[] { },
        //    MaterialEfficiencyCount = new int[] { },
        //    MaterialsCraftingUID = null,
        //    MaterialsCraftingCount = null,
        //    MaterialType = SlimeMaterial.MaterialTypes.Находимый
        //};
        //ef.Add(effect);
        //SlimeMaterial.Materials effects = new SlimeMaterial.Materials
        //{
        //    materials = ef
        //};

        //string js = JsonUtility.ToJson(effects, true);
        //File.WriteAllText(Application.persistentDataPath + "/" + "Materials" + ".json", js);




        //List<SlimeEffect> ef = new List<SlimeEffect>();
        //SlimeEffect effect = new SlimeEffect()
        //{
        //    effectUID = 1,
        //    effectName = "Вес",
        //    effectInfo = "Описания пока-что нету",
        //    effectImage = null,
        //    effectValue = "0.01f",
        //    effectValueType = SlimeEffect.EffectValueTypes.Float
        //};
        //ef.Add(effect);
        //SlimeEffect.Effects effects = new SlimeEffect.Effects
        //{
        //    effects = ef
        //};

        //string js = JsonUtility.ToJson(effects, true);
        //File.WriteAllText(Application.persistentDataPath + "/" + "Effects" + ".json", js);

    }

    public void Click()
    {

    }

    public enum OperationType
    {
        Save,
        Upload
    }

    [System.Serializable]
    public class OnDevelop_SelectItem : UnityEvent<SlimeMaterial, List<float>> { }
}
