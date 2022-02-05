using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientAnimator : MonoBehaviour
{
    public Color[] colors; // массив цветов, которые могут быть
    private LineRenderer line;
    [SerializeField]
    private Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public Color color_0, color_1, color_2;
    public float alpha;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        gradient = line.colorGradient;

        colorKey = line.colorGradient.colorKeys;
        alphaKey = line.colorGradient.alphaKeys;
    }
    void Update()
    {
        colorKey[0].color = color_0;
        colorKey[1].color = color_1;
        colorKey[2].color = color_2;
        gradient.SetKeys(colorKey, alphaKey);
        line.colorGradient = gradient;
        line.material.SetTextureOffset("_MainTex", new Vector2(line.material.GetTextureOffset("_MainTex").x - 0.01f, 0));
    }
}
