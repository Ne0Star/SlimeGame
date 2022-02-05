using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryProjector : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool blocker;
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    public void ShowTrajectory(Vector2 origin, Vector2 speed, Rigidbody2D body, float totalMass)
    {
        Vector2[] points = new Vector2[3];
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + new Vector2(Physics.gravity.x, Physics.gravity.y * totalMass) * time * time - body.velocity;
            lineRenderer.SetPosition(i, points[i]);
        }
    }
    private float direction;
    public void Move(Vector2 pos)
    {
        direction = (pos.x > 0) ? 0.006f : -0.006f;
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(lineRenderer.material.GetTextureOffset("_MainTex").x + direction, 0));
    }
    public IEnumerator HideTrajectory()
    {
        for (float i = 0; i < 10f; i += 0.2f)
        {

            lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(lineRenderer.material.GetTextureOffset("_MainTex").x + -direction, 0));

            yield return new WaitForFixedUpdate();
        }
        lineRenderer.positionCount = 0;
    }
}
