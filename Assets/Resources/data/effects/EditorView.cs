#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;



[CustomEditor(typeof(SlimeMaterial))]
public class EditorView : Editor
{
    private SlimeMaterial currentMaterial;
    private SlimeEffects effectsView;

    private List<float> effectNewValue;
    private float defaultValue = 0.001f;
    private SlimeMaterial.CraftInfo craftInfo = new SlimeMaterial.CraftInfo();
    private bool show_0, show_1 = true;
    private Vector2 scrollPosition = Vector2.zero;

    private void OnEnable()
    {
        currentMaterial = (SlimeMaterial)target;


        TextAsset texts = (TextAsset)Resources.Load<TextAsset>("data/AllEffects");
        effectsView = JsonUtility.FromJson<SlimeEffects>(texts.text);

        SetEffect();
    }
    private void SetEffect()
    {
        effectNewValue = currentMaterial.MaterialEffecting;
        if (currentMaterial.MaterialEffecting == null || currentMaterial.MaterialEffecting.Count == 0)
        {
            Debug.Log("Нету");
            effectNewValue = new List<float>();
            for (int i = 0; i < effectsView.effect.Count; i++)
            {
                effectNewValue.Add(defaultValue);
            }
            currentMaterial.MaterialEffecting = effectNewValue;
        }
    }
    public override void OnInspectorGUI()
    {
        currentMaterial.MaterialImage = (Sprite)EditorGUILayout.ObjectField("Спрайт", currentMaterial.MaterialImage, typeof(Sprite), false);
        currentMaterial.MaterialName = EditorGUILayout.TextField("Название: ", currentMaterial.MaterialName);
        EditorGUILayout.LabelField("Описание:");
        currentMaterial.MaterialInfo = EditorGUILayout.TextField(currentMaterial.MaterialInfo, GUILayout.Height(50f));

        show_0 = EditorGUILayout.ToggleLeft("Показать состав (" + currentMaterial.MaterialCrafting.Count + ") элементов", show_0);
        if (show_0)
        {
            float height = (currentMaterial.MaterialCrafting.Count > 0) ? 100 : 0f;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(height));
            try
            {
                foreach (SlimeMaterial.CraftInfo m in currentMaterial.MaterialCrafting)
                {
                    m.material = (SlimeMaterial)EditorGUILayout.ObjectField("Материал для создания: ", m.material, typeof(SlimeMaterial), false);
                    m.materialCount = EditorGUILayout.IntField("Количество для крафта: ", m.materialCount);

                    if (GUILayout.Button("Удалить"))
                    {
                        currentMaterial.MaterialCrafting.Remove(m);
                    }
                }

            }
            catch { }
            GUILayout.EndScrollView();
            EditorGUILayout.LabelField("Добавить составной материал: ");
            craftInfo.material = (SlimeMaterial)EditorGUILayout.ObjectField("Материал для создания: ", craftInfo.material, typeof(SlimeMaterial), false);
            craftInfo.materialCount = EditorGUILayout.IntField("Количество для создания: ", craftInfo.materialCount);
            if (GUILayout.Button("Добавить"))
            {
                currentMaterial.MaterialCrafting.Add(craftInfo);
                craftInfo = new SlimeMaterial.CraftInfo();
            }
        }
        show_1 = EditorGUILayout.ToggleLeft("Показать еффекты ", show_1);
        if (show_1)
        {
            if (effectNewValue.Count < effectsView.effect.Count) // Если добавился элемент
                for (int i = effectNewValue.Count; i < effectsView.effect.Count; i++)
                {
                    effectNewValue.Add(defaultValue);
                }
            if (effectNewValue.Count > effectsView.effect.Count) // Если удалился элемент
                for (int i = 0; i < effectNewValue.Count; i++)
                {

                    if (i >= effectsView.effect.Count)
                    {
                        effectNewValue.RemoveAt(i);
                    }//

                }
            if (effectNewValue.Count == effectsView.effect.Count) // Если равны
                for (int i = 0; i < effectsView.effect.Count; i++)
                {
                    SlimeEffects.Effect e = effectsView.effect[i];

                    GUIStyle style_0 = new GUIStyle();
                    style_0.fontStyle = FontStyle.Bold;
                    style_0.normal.textColor = Color.white;
                    EditorGUILayout.LabelField(e.effectName, style_0);
                    //EditorGUILayout.LabelField("Название эффекта: " + );
                    EditorGUILayout.LabelField(e.effectInfo);

                    //e.effectImage = (Sprite)EditorGUILayout.ObjectField(e.effectImage, typeof(Sprite), false, GUILayout.Height(50), GUILayout.Width(50));
                    effectNewValue[i] = EditorGUILayout.FloatField("Значение: ", effectNewValue[i]);
                }

            if (GUILayout.Button("Сохранить"))
            {
                currentMaterial.MaterialEffecting = effectNewValue;
            }
        }

#if UNITY_EDITOR
        if (GUI.changed)
        {
            EditorUtility.SetDirty((SlimeMaterial)target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

#endif
    }
}
#endif