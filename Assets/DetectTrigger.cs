using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    [Range(0.001f, 1f)]
    public float timeStep;
    [Range(1, 100)]
    public int maxTarget;

    private int tempTarget = 0;
    private float time = 0f;
    private List<Transform> trans;

    void Start()
    {
        trans = new List<Transform>();
    }

    public void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "Enemu")
        {
            if (!(trans.Contains(c.transform)))
            {
                if (tempTarget < maxTarget && !(tempTarget == maxTarget))
                {
                    tempTarget++;
                    trans.Add(c.transform);
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        trans.Remove(collision.transform);
        tempTarget--;
    }

    private void FixedUpdate()
    {

        time += timeStep;
        if (time >= 1f)
        {
            if (trans.Count <= maxTarget)
            {
                foreach (Transform t in trans)
                {
                    GameManager.Instance?.eventManager?.Onattack?.Invoke(t.position, false);
                }
            }
            else
            {
                for (int i = 0; i < trans.Count; i++)
                {
                    if ((i + 1) > maxTarget)
                    {

                        trans.RemoveAt(i);
                    }
                }
                return;
            }
            time = 0f;
        }
    }
}
