using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Slime : StandartSlime
{
    public float DampingRatio, Frequency;
    public SpringJoint2D[] allJoint_1, allJoint_2, allJoint_3;


    [Range(0.0f, 100.0f)]
    public float persent;
    public GameObject attackobject;

    public Vector2 dis;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    private Vector2 velocityPower;
    private Rigidbody2D m_body;

    private void Start()
    {

        int tempLength = 0;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).GetComponent<SpringJoint2D>())
            {
                tempLength++;
            }
        }
        allJoint_1 = new SpringJoint2D[tempLength];
        allJoint_2 = new SpringJoint2D[tempLength];
        allJoint_3 = new SpringJoint2D[tempLength];
        for (int i = 0; i < tempLength; i++)
        {
            allJoint_1[i] = transform.parent.GetChild(i).GetComponent<SpringJoint2D>();
            allJoint_2[i] = transform.parent.GetChild(i).GetComponent<SpringJoint2D>();
            allJoint_3[i] = transform.parent.GetChild(i).GetComponent<SpringJoint2D>();
            allJoint_1[i].dampingRatio = DampingRatio;
            allJoint_1[i].frequency = Frequency;
            allJoint_2[i].dampingRatio = DampingRatio;
            allJoint_2[i].frequency = Frequency;
            allJoint_3[i].dampingRatio = DampingRatio;
            allJoint_3[i].frequency = Frequency;
        }

        _jump = new TrajectoryJump();
        m_body = gameObject.GetComponent<Rigidbody2D>();
        GameManager.Instance?.eventManager?.Onattack?.AddListener(Attack);
    }
    public void Attack(Vector2 xy, bool b)
    {
        Debug.Log("attack");
        int index = 1;
        bool bonus = Random.Range(1, 101) <= persent;
        if (bonus)
        {
            index = 5;
        }
        for (int i = 0; i < index; i++)
        {
            GameObject g = Instantiate(attackobject, new Vector3(m_body.position.x, m_body.position.y, 0), Quaternion.identity);
            SpriteRenderer render = g.GetComponent<SpriteRenderer>();
            g.GetComponent<Animator>().Play("animation");
            render.color = (bonus ? Color.green : Color.red);
            g.SetActive(true);
            Rigidbody2D attackBody = g.GetComponent<Rigidbody2D>();
            Vector2 v = Vector2.zero;
            if (bonus)
            {
                xy = new Vector2(xy.x + Mathf.PingPong(Time.time, Mathf.Sin(6f)), xy.y + Mathf.PingPong(Time.time, Mathf.Cos(6f)));
                v = (xy - m_body.position) * Mathf.Sqrt(Random.Range(0f, 20f));
            }
            else
            {
                v = (xy - m_body.position) * 4f;
            }

            attackBody.AddForce(v, ForceMode2D.Impulse);
        }
    }
    float GaussApproximation(float x, float y)
    {
        return Mathf.Cos(x) / Mathf.Exp(1.5f + Mathf.Pow(x / y, 2f));
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_body.AddForce(new Vector2(TotalMass * 2f, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_body.AddForce(new Vector2(-TotalMass * 2f, 0));
        }

        if (startPosition != Vector2.zero && currentPosition != Vector2.zero && unlock)
        {
            m_body.velocity = Vector2.zero;
            velocityPower = (startPosition - currentPosition) * JumpPower;
            velocityPower = Vector2.ClampMagnitude(velocityPower, JumpPower);

            _jump.ShowTrajectory(m_body.position, velocityPower, TotalMass, m_body.velocity, 100, m_body.gravityScale);

        }

        if (Input.GetMouseButtonDown(0))
        {

            StopAllCoroutines();
            //StartCoroutine(Coldown(false));
            startPosition = Camera.main.WorldToScreenPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            currentPosition = Camera.main.WorldToScreenPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && unlock)
        {
            StopAllCoroutines();



            //string s;
            //s = velocityPower.x < 0 ? "left" : "right";
            //anim.Play("blink", 1);
            //anim.Play(s);

            // StartCoroutine(Coldown(true));
            m_body.AddForce(velocityPower, ForceMode2D.Impulse);
            InvokeRepeating("Coldown", 0, 1f);
            ClearCash();
        }
    }

    private bool unlock = true;
    [SerializeField]
    private float time;
    private void Coldown()
    {
        time += 1;
        unlock = false;
        if (time == 1)
        {
            CancelInvoke("Coldown");
            _jump.HideTrajectory();
            unlock = true;
            time = 0f;
        }
    }
    private IEnumerator Coldown(bool open)
    {
        if (open)
        {
            for (float i = 0.5f; i < 1f; i += Time.deltaTime / 0.4f)
            {
                Time.timeScale = i;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
            _jump.HideTrajectory();
        }
        else
        {
            for (float i = Time.timeScale; i > 0.5f; i -= Time.deltaTime / 1f)
            {
                Time.timeScale = i;
                yield return new WaitForFixedUpdate();
            }
        }
    }
    private void ClearCash()
    {
        startPosition = Vector2.zero;
        currentPosition = Vector2.zero;
        velocityPower = Vector2.zero;
    }
}
