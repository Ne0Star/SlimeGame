using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;

public class Grasses : MonoBehaviour
{
    public SpriteShapeController shape;
    private EdgeCollider2D edgeCollider;
    private Material fill;


    void Start()
    {

        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        shape = gameObject.GetComponent<SpriteShapeController>();
        fill = gameObject.GetComponent<SpriteShapeRenderer>().materials[1];
    }

    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        fill.SetVector("positions", collision.transform.position);
    }
}
