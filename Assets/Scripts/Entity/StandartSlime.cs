using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceManager;
using static StaticData;



/// <summary>
/// Базовый слайм, Может прыгать
/// </summary>
public abstract class StandartSlime : MonoBehaviour
{
    public float JumpPower, TotalMass;
    public ITajectoryJump _jump;
}



/// <summary>
/// Свободно перемещается в воде, Не может прыгать
/// </summary>
public abstract class WaterSlime : MonoBehaviour
{

}
/// <summary>
/// Свободно перемещается в магме, может подлетать на небольшую высоту
/// </summary>
public abstract class FireSlime : MonoBehaviour
{

}