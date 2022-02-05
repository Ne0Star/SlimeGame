using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debbuger : MonoBehaviour
{
    private Text fps, fpsView;

    void Start()
    {
        fpsView = gameObject.transform.GetChild(0).GetComponent<Text>();
        fps = gameObject.transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        fps.text = 1 / Time.deltaTime + "";
    }
}
