using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSetting : MonoBehaviour
{
    //
    // Публичные переменные
    //

    public Dropdown GraphicMode, PhysicMode, fps;
    public Toggle sfx, musik;


    //
    // Приватные переменные
    //
    private Button button;


    private void Start()
    {
        UpdateData();

        button.onClick.AddListener(() => Save(sfx.isOn, musik.isOn, GraphicMode.value, PhysicMode.value));

    }

    //
    // Обновить данные, получив их из файла
    //
    private void OnEnable()
    {
        UpdateData();


    }
    private void UpdateData()
    {
        button = gameObject.GetComponent<Button>();
        SettingData data = null;
        data = GameManager.Instance?.saveManager?.LoadSetting();
        SettingData settingData = data;
        GraphicMode.value = (int)settingData.graphicsPreset;
        PhysicMode.value = (int)settingData.physicsPreset;
        sfx.isOn = settingData.SFX;
        musik.isOn = settingData.MUSIK;
        fps.value = settingData.FPSIndex;
    }
    private void OnDisable()
    {

    }
    //
    // Сохранение настроек
    //
    public void Save(bool SFX, bool MUSIK, int graphicsPreset, int physicsPreset)
    {
        SettingData settingData = new SettingData
        {
            SFX = SFX,
            MUSIK = MUSIK,
            graphicsPreset = (SettingData.GraphicsPreset)graphicsPreset,
            physicsPreset = (SettingData.PhysicsPreset)physicsPreset,
            FPSIndex = fps.value,
            FPS = int.Parse(fps.options[fps.value].text)
        };
        GameManager.Instance?.saveManager?.SaveSetting(settingData);
    }

}
