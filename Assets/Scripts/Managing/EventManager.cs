using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SaveManager;
[System.Serializable]
public class EventManager
{

    public OnLoadData OnloadData;
    public OnSaveData OnsaveData;
    public OnAttack Onattack;
    public EventManager()
    {
        OnsaveData = new OnSaveData();
        OnloadData = new OnLoadData();
        Onattack = new OnAttack();
    }

    [System.Serializable]
    public class OnAttack : UnityEvent<Vector2, bool> { }
    [System.Serializable]
    public class OnLoadData : UnityEvent<int> { }
    [System.Serializable]
    public class OnSaveData : UnityEvent<int> { }
    [System.Serializable]
    public class OnEnterAttackArea : UnityEvent<Collider2D> { }
}
