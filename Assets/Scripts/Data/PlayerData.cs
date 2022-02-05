using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;

//
// Отвечает за хранение данных об игроке
//

public class PlayerData
{
    
    public PlayerData()
    {
        
    }

    public enum SlimeEvoType
    {
                 //|| Вес     ||

        Standart,//|| Средний ||
        Air,     //|| Легкий  ||
        Fire,    //|| Легкий  ||
        Ground,  //|| Тяжелый ||
        Floral   //|| Средний ||
    }
    public enum Air
    {

    }
}
