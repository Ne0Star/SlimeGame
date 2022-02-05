using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed;
    public AnimationCurve interpolator;
    public Transform target;
    private void Update()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), interpolator.Evaluate(Time.deltaTime * speed));
    }
}
