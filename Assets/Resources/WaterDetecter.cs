﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вошел " + collision.name);
    }
}
