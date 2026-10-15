using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.enabled = false;
        lr.startWidth = 0.025f;
        lr.endWidth = 0.025f;
    }

    public void Play(Vector3 from, Vector3 to)
    {
        lr.enabled = true;

        lr.SetPosition(0, from);
        lr.SetPosition(1, to);
    }

    public void Stop()
    {
        lr.enabled = false;
    }

    public void ChangeColor(float maxTime,float time)
    {
        Color mycolor = new Color(time / maxTime, 0, 0, 1);
        lr.SetColors(mycolor, mycolor);
    }
}
