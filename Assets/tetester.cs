using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetester : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    float F(float x, float a, float c, float b)
    {
        bool temp = (x < 0.6f) ? true : false;
        switch (temp)
        {
            case true:
                return ((a + x) * 3) + (b * 2) + c;
                break;
            case false:
                return (x - a) / (x - c);
                break;
            default:
                return (x / c) + (x / a);
                break;
        }
    }
}
