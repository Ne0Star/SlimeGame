using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceManager;

public class TrajectoryJump : ITajectoryJump
{
    private LineRenderer LineRenderer;
    private GameObject LineRenderer_Object;

    public TrajectoryJump()
    {
        LineRenderer_Object = GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        LineRenderer_Object.name = "LineRenderer";

        LineRenderer = LineRenderer_Object.AddComponent<LineRenderer>();
        LineRenderer.textureMode = LineTextureMode.Tile;
        LineRenderer.material = (Material)Resources.Load<Material>("materials/Line");
    }

    public void HideTrajectory()
    {
        if (LineRenderer == null)
            return;
        LineRenderer.positionCount = 0;
    }

    public void Jump()
    {

    }
    public void ShowTrajectory(Vector2 jumpStart, Vector2 jumpPower, float mass, Vector2 bodyVelocity, int length, float gravity)
    {
        if (LineRenderer == null)
            return;
        Vector2[] points = new Vector2[length];
        LineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * Time.fixedUnscaledDeltaTime;
            points[i] = jumpStart + (jumpPower + bodyVelocity) * time + (Physics2D.gravity * mass * mass) * time * time / 2f * gravity;
            LineRenderer.SetPosition(i, points[i]);
        }
    }
}
