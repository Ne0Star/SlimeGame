using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableOne : MonoBehaviour
{

    public EventManager events;

    private GameManager manager;
    void Start()
    {
        manager = GameManager.Instance;
        events = manager.eventManager;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
