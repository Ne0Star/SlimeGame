using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyedObject : MonoBehaviour
{
    [SerializeField]
    private GameObject hits_Bar;
    [SerializeField]
    private float hit_point = 10f;
    private SpriteRenderer hit_point_R;
    float start_hit_point, startWidth;

    private void Start()
    {
        start_hit_point = hit_point;
        hit_point_R = hits_Bar.transform.GetChild(0).GetComponent<SpriteRenderer>();
        startWidth = hit_point_R.size.x;
        Debug.Log(startWidth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Attacker")
        {
            if (hit_point <= 0)
            {
                Destroy(gameObject);
                return;
            }
            hits_Bar.transform.rotation = new Quaternion(transform.rotation.x * -1f, transform.rotation.y * -1f, transform.rotation.z * -1f, 1f);
            hit_point -= 0.2f;
            hit_point_R.size = new Vector3(startWidth / start_hit_point * hit_point, hit_point_R.size.y, 0);
        }
        else
        {
            return;
        }
    }
}
