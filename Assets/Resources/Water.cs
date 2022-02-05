using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class Water : MonoBehaviour
{
    public SpriteShapeController shape;
    public Spline spline;
    public ShapeTangentMode waterMode;
    public float splineLeftRight;
    public Vector2 bound;

    [SerializeField]
    private Material fillMaterial;

    public bool useGaus;
    [SerializeField]
    private float
        springConstant = .02f,
        damping = .1f,
        spread = .1f,
        waterSimulateColdownTime,
        coldownTime;
    public float timer;
    float[] velocities, accelerations;
    Vector2[] vertices;
    [Header("GaussSetting")]
    public float impactDepth, impactWidth;
    public Vector3 gauss = new Vector3(1f, 1.5f, 2f);
    private bool blocker = false;
    public float tt;
    [SerializeField]
    private float boundHeight;
    private GameManager MANAGER;
    [SerializeField]
    private UpdateType updateType;
    public enum UpdateType
    {
        Fixed,
        Standart
    }

    private void Start()
    {
        MANAGER = GameManager.Instance;
        shape = gameObject.GetComponent<SpriteShapeController>();
        spline = shape.spline;

        fillMaterial = gameObject.GetComponent<SpriteShapeRenderer>().material;

        boundHeight = gameObject.GetComponent<SpriteShapeRenderer>().bounds.size.y;

        vertices = new Vector2[spline.GetPointCount()];
        for (int i = 1; i < spline.GetPointCount() - 2; i++)
        {
            vertices[i] = spline.GetPosition(i);

            spline.SetTangentMode(i, waterMode);
            spline.SetLeftTangent(i, new Vector3(-splineLeftRight, 0, 0));
            spline.SetRightTangent(i, new Vector3(splineLeftRight, 0, 0));

        }
        leftDeltas = new float[spline.GetPointCount()];
        rightDeltas = new float[spline.GetPointCount()];
        velocities = new float[spline.GetPointCount()];
        accelerations = new float[spline.GetPointCount()];

        updateType = MANAGER.saveManager.EndSavedData.FPS > 60 ? UpdateType.Fixed : UpdateType.Standart;
    }
    float[] leftDeltas, rightDeltas;

    private void FixedUpdate()
    {
        if (updateType == UpdateType.Standart)
            return;
        Calculate();
    }
    private void Calculate()
    {
        if (timer <= 0) return;
        timer -= Time.unscaledDeltaTime;

        for (int i = 1; i < spline.GetPointCount() - 2; i++)
        {
            float force = springConstant * (vertices[i].y - bound.y) + velocities[i] * damping * tt;
            accelerations[i] = -force;
            vertices[i].y += velocities[i];
            velocities[i] += accelerations[i];
        }
        for (int i = 1; i < spline.GetPointCount() - 2; i++)
        {
            if (i > 0)
            {
                leftDeltas[i] = spread * (vertices[i].y - vertices[i - 1].y);
                velocities[i - 1] += leftDeltas[i];
            }

            if (i < spline.GetPointCount() - 1)
            {
                rightDeltas[i] = spread * (vertices[i].y - vertices[i + 1].y);
                velocities[i + 1] += rightDeltas[i];
            }
        }
        for (int i = 0; i < spline.GetPointCount() - 2; i++)
        {
            if (i > 0)
                velocities[i - 1] += leftDeltas[i];
            if (i < spline.GetPointCount() - 3)
                velocities[i + 1] += rightDeltas[i];
        }
        for (int i = 1; i < spline.GetPointCount() - 2; i++)
        {
            spline.SetPosition(i, new Vector2(spline.GetPosition(i).x, vertices[i].y));
        }
    }
    private void Update()
    {
        if (updateType != UpdateType.Standart)
            return;
        Calculate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!blocker)
        {
            StartCoroutine(Coldown(coldownTime));
            StartCoroutine(SimulateColdown(waterSimulateColdownTime, collision, true));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!blocker)
        {
            StartCoroutine(Coldown(coldownTime));
            StartCoroutine(SimulateColdown(waterSimulateColdownTime, collision, false));
        }
    }
    private void Adapter(Collider2D collision, bool open)
    {

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        Splash(collision, rb.velocity.y * rb.mass * 4f, open);
    }

    private void Splash(Collider2D collision, float force, bool open)
    {
        timer = 3f;
        float radius = 0f;
        if (collision.GetComponent<CircleCollider2D>())
        {
            radius = collision.GetComponent<CircleCollider2D>().radius;
        }
        else
        {
            radius = collision.bounds.max.x - collision.bounds.min.x;
        }

        Vector2 center = new Vector2(collision.bounds.center.x, transform.TransformPoint(bound).y);
        if (open)
            fillMaterial.SetVector("_Velocity", Vector2.Lerp(Vector2.zero, collision.attachedRigidbody.velocity, timer));
        for (int i = 1; i < spline.GetPointCount(); i++)
        {
            if (useGaus)
            {
                var dist = (spline.GetPosition(i).x - center.x) / radius;

                var gauss = GaussApproximation(dist, force) * impactDepth;
                velocities[i] = force * gauss;
            }
            else
            {
                if (PointInsideCircle(transform.TransformPoint(vertices[i]), center, radius))
                {
                    velocities[i] = force / boundHeight;
                    //if (open)
                    //    velocities[i] = -Mathf.Abs(force - boundHeight) / boundHeight;
                    //if (!open)
                    //    velocities[i] = Mathf.Abs(force - boundHeight) / boundHeight;
                }
            }
        }
    }

    private IEnumerator Coldown(float time)
    {
        blocker = true;
        yield return new WaitForSeconds(time);
        blocker = false;
    }
    private IEnumerator SimulateColdown(float time, Collider2D collision, bool exitOpen)
    {
        yield return new WaitForSeconds(time);
        Adapter(collision, exitOpen);
    }
    bool PointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        return Vector2.Distance(point, center) < radius;
    }
    float GaussApproximation(float x, float y)
    {
        return Mathf.Cos(x) / Mathf.Exp(gauss.x + Mathf.Pow(x / y, gauss.z));
    }
}
