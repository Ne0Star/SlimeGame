using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaterParticle : MonoBehaviour
{
    public Vector2 Position;
    public Vector2 Velocity;
    public float Orientation;
    public WaterParticle(Vector2 position, Vector2 velocity, float orientation)
    {
        Position = position;
        Velocity = velocity;
        Orientation = orientation;
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
}
