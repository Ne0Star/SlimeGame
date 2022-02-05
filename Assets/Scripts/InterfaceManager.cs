using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticData;

public class InterfaceManager
{        
    /// <summary>
    /// Тип поведения: Прыжок с траекторией
    /// </summary>
    public interface ITajectoryJump
    {
        /// <summary>
        /// Показать траекторию на основе силы
        /// </summary>
        void ShowTrajectory(Vector2 jumpStart, Vector2 jumpDirection, float mass, Vector2 bodyVelo, int length, float gravity);
        /// <summary>
        /// Очистить траекторию
        /// </summary>
        void HideTrajectory();
    }
    public interface IControlThePlayer
    {
        void ChangeController(ControllerType type);
    }
}
