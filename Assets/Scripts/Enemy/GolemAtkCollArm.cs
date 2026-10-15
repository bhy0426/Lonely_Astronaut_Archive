using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAtkCollArm : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    void Update()
    {
        transform.localPosition = new Vector3(offsetX, offsetY, transform.position.z);
    }
}
