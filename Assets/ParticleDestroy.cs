using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public float time = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            Destroy(gameObject);
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            Destroy(gameObject);
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Destroy(gameObject);
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        time += 0.01f;
        if (time > 2f)
            Destroy(gameObject);
    }
}
